using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class KnapsackData
    {
        public List<Item> AvailableItems { get; set; }
        public double MaxWeight { get; set; }

        public KnapsackData(int maxWeight) {
            AvailableItems = new List<Item>();
            MaxWeight = maxWeight;
        }
    }
}
