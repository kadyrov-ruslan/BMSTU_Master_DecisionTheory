using System;
using System.Collections.Generic;
using DES.PopulationGrowth.Events;
using MathNet.Numerics.Distributions;

namespace DES.PopulationGrowth.Objects
{
    public abstract class Individual
    {
        public int Age { get; set; }
        public int RelationAge { get; set; }
        public int LifeTime { get; set; }
        public double TimeChildren { get; set; }
        public Individual Couple { get; set; }

        protected Individual(int age)
        {
            Age = age;
        }

        public void FindCouple(IEnumerable<Individual> population, int currentTime, Dictionary<Event, IDistribution> distributions)
        {
            foreach (var candidate in population)
                if (SuitablePartner(candidate) &&
                    candidate.SuitableRelation() &&
                    ((ContinuousUniform)distributions[Event.BirthEngageDisengage]).Sample() <= 0.5)
                {
                    // Relate them
                    candidate.Couple = this;
                    Couple = candidate;
                    // Set time for having child
                    var childTime = ((Exponential)distributions[Event.TimeChildren]).Sample() * 100;
                    // They can have children on the simulated year: 'currentTime + childTime'.
                    candidate.TimeChildren = currentTime + childTime;
                    TimeChildren = currentTime + childTime;
                    break;
                }
        }

        public bool SuitableRelation()
        {
            return Age >= RelationAge && Couple == null;
        }

        public bool SuitablePartner(Individual individual)
        {
            return ((individual is Male && this is Female) ||
                   (individual is Female && this is Male)) &&
                   Math.Abs(individual.Age - Age) <= 5;
        }

        public bool Engaged
        {
            get { return Couple != null; }
        }

        public void Disengage()
        {
            Couple.Couple = null;
            Couple = null;
            TimeChildren = 0;
        }

        public bool EndRelation(Dictionary<Event, IDistribution> distributions)
        {
            var sample = ((ContinuousUniform)distributions[Event.BirthEngageDisengage]).Sample();

            if (Age >= 14 && Age <= 20 && sample <= 0.7)
                return true;
            if (Age >= 21 && Age <= 28 && sample <= 0.5)
                return true;
            if (Age >= 29 && sample <= 0.2)
                return true;

            return false;
        }

        public override string ToString()
        {
            return $"Возраст: {Age} лет; продолжительность жизни {LifeTime} лет.";
        }
    }
}