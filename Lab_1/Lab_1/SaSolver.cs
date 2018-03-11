using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class SaSolver
    {
        private double temperature;
        private double coolingFactor;
        private double endingTempreture;
        private int samplingSize;
        private static double ALPHA = 1000000;
        //private final Random random = new Random(System.nanoTime());
        private KnapsackData data;

        public SaSolver(int samplingSize, double initTemerature, double endingTempreture, double coolingFactor) 
        {
            samplingSize = samplingSize;
            endingTempreture = endingTempreture;
            coolingFactor = coolingFactor;
            temperature = initTemerature;
        }

        public Solution Solve(KnapsackData data)
        {
            this.data = data;
            long start = DateTime.UtcNow.Millisecond;
            var current = new BinarySolution(data.AvailableItems.Count);
            var best = current;
            current.UpdateFitness(data, ALPHA);

            while (temperature > endingTempreture) 
            {
                for (var m = 0; m < samplingSize; m++) {
                    current = GetNextState(current);
                    if (current.Fitness < best.Fitness) {
                        best = current;
                    }
                }
                Cool();
            }

            long end = DateTime.UtcNow.Millisecond;
            List<Item> pickedItem = generateSolution(data, best);
            return new Solution(pickedItem, end - start);
        }

        private BinarySolution GetNextState(BinarySolution current) 
        {
            var newSolution = GetNeighbour(current);
            var delta = newSolution.Fitness - current.Fitness;
            if (delta < 0) 
                return newSolution;
            else 
            {
                double x = Math.random();
                if (x < Math.Exp(-delta / temperature)) 
                    return newSolution;
                 else 
                    return current;
            }
        }

        private BinarySolution GetNeighbour(BinarySolution current) 
        {
            var mutated = new BinarySolution(current);
            int x = random.nextInt(current.getSize());
            mutated.Flip(x);
            mutated.UpdateFitness(data, ALPHA);
            return mutated;
        }

        private void Cool()
        {
            temperature *= coolingFactor;
        }
    }
}
