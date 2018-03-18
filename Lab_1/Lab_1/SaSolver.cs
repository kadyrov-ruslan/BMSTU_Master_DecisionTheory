using System;
using System.Collections.Generic;

namespace Lab_1
{
    public class SaSolver : IKnapsackStrategy
    {
        private double _temperature;
        private readonly double _coolingFactor;
        private readonly double _endingTemperature;
        private readonly int _samplingSize;

        private const double Alpha = 1000000;

        private KnapsackData _data;

        public SaSolver(int samplingSize, double initTemerature, double endingTempreture, double coolingFactor)
        {
            _samplingSize = samplingSize;
            _endingTemperature = endingTempreture;
            _coolingFactor = coolingFactor;
            _temperature = initTemerature;
        }

        public Solution Solve(KnapsackData data)
        {
            _data = data;
            long start = DateTime.UtcNow.Millisecond;
            var current = new BinarySolution(data.AvailableItems.Count);
            var best = current;
            current.UpdateFitness(data, Alpha);

            while (_temperature > _endingTemperature)
            {
                for (var m = 0; m < _samplingSize; m++)
                {
                    current = GetNextState(current);
                    if (current.Fitness < best.Fitness)
                        best = current;
                }

                Cool();
            }

            long end = DateTime.UtcNow.Millisecond;
            var pickedItem = GenerateSolution(data, best);
            return new Solution(pickedItem, end - start);
        }

        private BinarySolution GetNextState(BinarySolution current)
        {
            var newSolution = GetNeighbour(current);
            var delta = newSolution.Fitness - current.Fitness;
            if (delta < 0) return newSolution;

            var rnd = new Random();
            var x = rnd.NextDouble();
            if (x < Math.Exp(-delta / _temperature))
                return newSolution;
            return current;
        }

        private BinarySolution GetNeighbour(BinarySolution current)
        {
            var mutated = new BinarySolution(current);
            var rnd = new Random();
            var x = rnd.Next(current.GetSize());
            mutated.Flip(x);
            mutated.UpdateFitness(_data, Alpha);
            return mutated;
        }

        private void Cool()
        {
            _temperature *= _coolingFactor;
        }

        public List<Item> GenerateSolution(KnapsackData knapsackData, BinarySolution solution)
        {
            var pickedItem = new List<Item>();
            for (var i = 0; i < knapsackData.AvailableItems.Count; i++)
                if (solution.GetBit(i) == 1)
                    pickedItem.Add(knapsackData.AvailableItems[i]);
            return pickedItem;
        }
    }
}