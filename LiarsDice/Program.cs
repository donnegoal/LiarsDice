using System;

namespace LiarsDice
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("Donnie");
            var player2 = new Player("Kevin");

            player1.WagerAction = new ConsoleInputWager(player1.Name);
            player2.WagerAction = new ConsoleInputWager(player2.Name);

            var game = new Game();
            game.AddPlayer(player1);
            game.AddPlayer(player2);

            game.Play();

            Console.ReadKey();            

        }

    }
}
