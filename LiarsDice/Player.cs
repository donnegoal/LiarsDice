using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dice;

namespace LiarsDice
{
    class Player
    {
        public string Name { get; set; }
        public DiceSet<StandardDie> DiceSet { get; set; }
        public Wager wager { get; set;  }

        public Player(string name)
        {
            Name = name;
            DiceSet = new DiceSet<StandardDie>();
        }

        public void RollDice()
        {
            DiceSet.RollAll();
        }

    }
}
