using System;

namespace LiarsDice
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new Player("Donnie", GetWagerFromConsoleInput);
            var player2 = new Player("Kevin", GetWagerFromConsoleInput);

            Game game = new Game();
            game.AddPlayer(player1);
            game.AddPlayer(player2);

            game.Play();

            Console.ReadKey();            

        }

        public static Wager GetWagerFromConsoleInput()
        {
            var wager = new Wager();

            Console.WriteLine("(R)aise or (C)hallenge?:");
            var answer = Console.ReadLine();

            if (answer == "R")
            {
                Console.WriteLine("How many dice?:");
                wager.DiceCount = int.Parse(Console.ReadLine());
                Console.WriteLine("Face value? (1-6):");
                wager.FaceValue = int.Parse(Console.ReadLine());
            }
            else if (answer == "C")
            {
                wager.isChallenge = true;
            }
            else
            {
                Console.WriteLine("Please type 'R' or 'C'");
                return GetWagerFromConsoleInput();
            }

            return wager;
        }
    }
}
