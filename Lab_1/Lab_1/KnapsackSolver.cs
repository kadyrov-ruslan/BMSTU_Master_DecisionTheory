using System.Collections.Generic;

namespace Lab_1
{
    public class KnapsackSolver
    {
        private KnapsackData _data;
        private Solution _bestSolution;
        private readonly IKnapsackStrategy _strategy;

        public KnapsackSolver(KnapsackData data, IKnapsackStrategy strategy) 
        {
            _data = data;
            _strategy = strategy;
        }

        private Solution Find() 
        {
            return _strategy.Solve(_data);
        }

        public Solution GetSolution() 
        {
            if (_bestSolution == null)
                _bestSolution = Find();

            return _bestSolution;
        }

        public long GetTakenTime() 
        {
            return GetSolution().TakenTime;
        }

        public List<Item> GetSelectedItem() {
            return GetSolution().PickedItem;
        }
    }
}
