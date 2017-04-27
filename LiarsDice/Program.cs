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
            var player1 = new Player("Donnie");
            var player2 = new Player("Kevin");

            Game game = new Game();
            game.AddPlayer(player1);
            game.AddPlayer(player2);

            game.PlayRound();

            Console.ReadKey();
            

        }
    }
}
