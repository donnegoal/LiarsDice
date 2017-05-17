using System;

namespace LiarsDice
{
    internal class ConsoleInputWager : IWagerable
    {
        private string _playerName;

        public ConsoleInputWager(string player1Name)
        {
            _playerName = player1Name;
        }

        public Wager GetWager()
        {
            Console.WriteLine($"{_playerName}, it's your turn!\n");

            while (true)
            {
                var wager = new Wager();

                Console.WriteLine("(R)aise or (C)hallenge?:");
                var answer = Console.ReadLine();

                switch (answer)
                {
                    case "R":
                        Console.WriteLine("How many dice?:");
                        wager.DiceCount = int.Parse(Console.ReadLine());
                        Console.WriteLine("Face value? (1-6):");
                        wager.FaceValue = int.Parse(Console.ReadLine());
                        break;
                    case "C":
                        wager.isChallenge = true;
                        break;
                    default:
                        Console.WriteLine("Please type 'R' or 'C'");
                        continue;
                }

                return wager;
            }
        }
    }
}