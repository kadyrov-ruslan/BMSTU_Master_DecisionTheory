namespace DES.PopulationGrowth.Objects
{
    public class Male: Individual
    {
        public Male(int age) : base(age)
        {
        }

        public override string ToString()
        {
            return base.ToString() + " Мужч.";
        }
    }
}