using System;
using System.Collections.Generic;
using DES.PopulationGrowth;
using DES.PopulationGrowth.Objects;

namespace DES
{
    internal class Program
    {
        private static void Main()
        {
            var population = new List<Individual>
            {
                new Male(2),
                new Female(2),
                //new Male(3),
                //new Female(4),
                //new Male(5),
                //new Female(3)

            };

            var sim = new Simulation(population, 180);
            sim.Execute();

            for (var i = 0; i < sim.Population.Count; i++)
                Console.WriteLine($"#{i + 1} Индивид {sim.Population[i]}");

            Console.ReadLine();
        }
    }
}
