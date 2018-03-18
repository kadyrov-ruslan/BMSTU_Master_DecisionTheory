using System;

namespace Lab_1
{
    public class BinarySolution
    {
        private readonly byte[] _chromosome;
        public double Fitness { get; set; }
        public double Weight { get; set; }

        public BinarySolution(int size) 
        {
            _chromosome = new byte[size];
            Fitness = double.MaxValue;
            Weight = long.MaxValue;
        }

        public BinarySolution(BinarySolution other) 
        {
            _chromosome = other._chromosome;
            Fitness = other.Fitness;
            Weight = other.Weight;
        }

        public byte GetBit(int position) 
        {
            return _chromosome[position];
        }

        public int GetSize()
        {
            return _chromosome.Length;
        }

        public void Flip(int position) 
        {
            _chromosome[position] = (byte) (_chromosome[position] ^ 1);
        }

        public void Shuffle() 
        {
            for (var i = 0; i < _chromosome.Length; i++) 
            {
                var random = new Random();
                var randomNumber = random.NextDouble();
                if (randomNumber > 0.3)
                    _chromosome[i] = 0;
                else
                    _chromosome[i] = 1;             
            }
        }

        public void UpdateFitness(KnapsackData data, double alpha) 
        {
            double sumVal = 0, sumWeight = 0;
            for (var i = 0; i < data.AvailableItems.Count; i++) 
            {
                var item = data.AvailableItems[i];
                if (GetBit(i) == 1)
                    sumWeight += item.Weight;
                else
                    sumVal += item.Value;
            }
            var violation = Math.Max(sumWeight / data.MaxWeight - 1, 0);
            Weight = sumWeight;
            Fitness = sumVal + alpha * violation;
        }
    }
}
