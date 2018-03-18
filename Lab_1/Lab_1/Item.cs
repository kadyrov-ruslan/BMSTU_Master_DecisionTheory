namespace Lab_1
{
    public class Item
    {
        public double Weight { get; set; }

        public double Value { get; set; }

        public Item(int weight, int value) 
        {
            Weight = weight;
            Value = value;
        }
    }
}
