using System.Collections.Generic;

namespace Lab_1
{
    public interface IKnapsackStrategy
    {
        Solution Solve(KnapsackData data);
        List<Item> GenerateSolution(KnapsackData knapsackData, BinarySolution solution);
    }
}
