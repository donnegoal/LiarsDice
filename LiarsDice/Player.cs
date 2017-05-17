using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dice;

namespace LiarsDice
{
    internal class Player
    {
        public string Name { get; set; }
        public DiceSet<StandardDie> DiceSet { get; set; }
        public Wager Wager { get; set;  }
        public IWagerable WagerAction { get; set; }

        public Player(string name)
        {
            Name = name;
            DiceSet = new DiceSet<StandardDie>();
        }

        public void RollDice()
        {
            DiceSet.RollAll();
        }

        public void MakeWager()
        {
            Wager = WagerAction.GetWager();
        }

    }
}
