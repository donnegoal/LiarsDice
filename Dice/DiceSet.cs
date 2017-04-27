﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class DiceSet<T> : IEnumerable where T : Die, new()
    {
        public List<T> Dice { get; private set; }

        public DiceSet(int diceCount = 5)
        {
            Dice = new List<T>();
            for (int i = 0; i < diceCount; i++)
            {
                Dice.Add(new T());
            }
        }

        public void RollAll()
        {
            Dice.ForEach(d => d.Roll());
        }

        public void AddDice(int diceCount = 1)
        {
            Dice.AddRange(Enumerable.Repeat(new T(), diceCount));
        }

        public void RemoveDice(int diceCount = 1)
        {
            Dice.RemoveRange(0, diceCount);
        }

        public IEnumerator GetEnumerator()
        {
            return Dice.GetEnumerator();
        }      

                 
     }
}
