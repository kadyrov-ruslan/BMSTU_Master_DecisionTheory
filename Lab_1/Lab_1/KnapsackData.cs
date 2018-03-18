using System.Collections.Generic;

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

        public void AddItem(Item item)
        {
            AvailableItems.Add(item);
        }
        public Item GetData(int index)
        {
            return AvailableItems[index];
        }
    }
}
