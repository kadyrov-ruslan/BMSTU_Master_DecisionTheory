using System.Collections.Generic;
using DES.PopulationGrowth.Events;
using DES.PopulationGrowth.Objects;
using MathNet.Numerics.Distributions;

namespace DES.PopulationGrowth
{
    public class Simulation
    {
        public List<Individual> Population { get; set; }
        public int Time { get; set; }
        private int _currentTime;
        private readonly Dictionary<Event, IDistribution> _distributions;

        public Simulation(IEnumerable<Individual> population, int time)
        {
            Population = new List<Individual>(population);
            Time = time;
            _distributions = new Dictionary<Event, IDistribution>
            {
                {Event.CapableEngaging, new Poisson(18)},
                {Event.BirthEngageDisengage, new ContinuousUniform()},
                {Event.GetPregnant, new Normal(28, 8)},
                {Event.ChildrenCount, new Normal(2, 6)},
                {Event.TimeChildren, new Exponential(8)},
                {Event.Die, new Poisson(70)},
            };
            foreach (var individual in Population)
            {
                // LifeTime
                individual.LifeTime = ((Poisson)_distributions[Event.Die]).Sample();
                // Готов к началу взаимоотношений
                individual.RelationAge = ((Poisson)_distributions[Event.CapableEngaging]).Sample();
                // Возраст при беременности (только для женщин)
                if (individual is Female)
                {
                    (individual as Female).PregnancyAge = ((Normal)_distributions[Event.GetPregnant]).Sample();
                    (individual as Female).ChildrenCount = ((Normal)_distributions[Event.ChildrenCount]).Sample();
                }
            }
        }

        public void Execute()
        {
            while (_currentTime < Time)
            {
                // Проверяем. что происходит с каждым индивидуумом в этом году
                for (var i = 0; i < Population.Count; i++)
                {
                    var individual = Population[i];
                    // Событие -> рождение
                    if (individual is Female && (individual as Female).IsPregnant)
                        Population.Add((individual as Female).GiveBirth(_distributions,
                            _currentTime));
                    // Событие -> проверяем, начинает ли кто-то
                    // взаимоотношения в этом году
                    if (individual.SuitableRelation())
                        individual.FindCouple(Population, _currentTime, _distributions);
                    // События, где наличие половозрелого индивидуума
                    // является обязательным требованием
                    if (individual.Engaged)
                    {
                        // Событие -> проверяем, есть ли взаимоотношения,
                        // которые прекращаются в этом году
                        if (individual.EndRelation(_distributions))
                            individual.Disengage();
                        // Событие -> проверяем, может ли сейчас пара иметь ребенка
                        if (individual is Female &&
                            (individual as Female).SuitablePregnancy(_currentTime))
                            (individual as Female).IsPregnant = true;
                    }

                    // Событие -> проверяем, умирает ли кто-то в этом году
                    if (individual.Age.Equals(individual.LifeTime))
                    {
                        // Case: индивидуум во взаимоотношениях (прекращение)
                        if (individual.Engaged)
                            individual.Disengage();
                        Population.RemoveAt(i);
                    }

                    individual.Age++;
                    _currentTime++;
                }
            }
        }
    }
}