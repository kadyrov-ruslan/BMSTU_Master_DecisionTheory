using System;

namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var knapsackData = new KnapsackData(50);
            knapsackData.AddItem(new Item(10, 60));
            knapsackData.AddItem(new Item(20, 120));
            knapsackData.AddItem(new Item(20, 110));
            knapsackData.AddItem(new Item(80, 150));
            knapsackData.AddItem(new Item(40, 210));
            knapsackData.AddItem(new Item(50, 100));
            knapsackData.AddItem(new Item(10, 100));
            knapsackData.AddItem(new Item(20, 100));
            knapsackData.AddItem(new Item(90, 400));

            Console.WriteLine("Starting processing...");
            var sasolver = new SaSolver(3, 4000.0, 0.00001, 0.9999);
            var solver = new KnapsackSolver(knapsackData, sasolver);
            var solution = solver.GetSolution();
            Console.WriteLine($"Result: taken time {solution.TakenTime}, picked items");
            foreach (var item in solution.PickedItem)
            {
                Console.WriteLine($"item: weight {item.Weight}, value {item.Value}");
            }
            Console.WriteLine($"Result: gained value {solution.GainedValue}, gained weight {solution.GainedWeight}");
            Console.ReadKey();
        }
    }
}
