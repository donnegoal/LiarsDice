using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiarsDice
{
    class Game
    {
        private LinkedList<Player> _players;
        private Func<Wager> _wagerAction;   

        public Game()
        {
            _players = new LinkedList<Player>();
        }

        public void AddPlayer(Player player)
        {
            _players.AddLast(player);
        }

        public void Play()
        {
            while (!IsOver())
                PlayRound();
        }

        public void PlayRound()
        {
            var round = new Round(_players);
            round.StartRound();

            while(!round.IsOver())
            {
                round.NextWager();
            }
            round.Close();
        }

        public bool IsOver()
        {
            return _players.Count == 1;
        }


    }
}
