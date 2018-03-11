using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class Solution
    {
        public List<Item> PickedItem { get; set; }
        public long GainedValue { get; set; }
        public long GainedWeight { get; set; }
        public long TakenTime { get; set; }

        public Solution(List<Item> pickedItem, long takenTime) 
        {
            PickedItem = pickedItem;
            GainedValue = -1;
            GainedWeight = -1;
            TakenTime = takenTime;
        }
    }
}
