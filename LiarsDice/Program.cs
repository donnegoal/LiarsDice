using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dice;

namespace LiarsDice
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player("Donnie");

            player.DiceSet.RollAll();

            player.DiceSet.Dice.ForEach(d => Console.WriteLine(d.currentValue));

            Console.ReadKey();

        }
    }
}
