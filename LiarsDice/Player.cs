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
        private Func<Wager> _wagerFunc;

        public Player(string name, Func<Wager> wagerFunc)
        {
            Name = name;
            _wagerFunc = wagerFunc;
            DiceSet = new DiceSet<StandardDie>();
        }

        public void RollDice()
        {
            DiceSet.RollAll();
        }

        public void MakeWager()
        {
            wager = _wagerFunc();
        }

    }
}
