using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dice
{
    public class StandardDie : Die
    {
        private static readonly Random random = new Random();

        public int sideCount { get; private set; }
        public int currentValue { get; private set; }

        public StandardDie()
        {
            sideCount = 6;
            Roll();
        }

        public void Roll() {
            currentValue = random.Next(1, sideCount + 1);
        }
    }
}
