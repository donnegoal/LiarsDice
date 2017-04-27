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
        

        public Game()
        {
            _players = new LinkedList<Player>();
        }

        public void AddPlayer(Player player)
        {
            _players.AddLast(player);
        }

        public void PlayRound()
        {
            var round = new Round(_players);

        }

        public void NextWager(Func<Wager> wagerFunction)
        {
            var wager = wagerFunction;
            
        }
    }
}
