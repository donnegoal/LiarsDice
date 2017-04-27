using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiarsDice
{
    class Round
    {
        private LinkedList<Player> _players;
        private LinkedListNode<Player> _activePlayer;
        private Player _winner;
        private Player _loser;

        public Round(LinkedList<Player> players)
        {
            _players = players;
        }

        public void StartRound()
        {
            foreach(var player in _players)
            {
                player.RollDice();
            }
        }

        public void OpeniningWager(Func<Wager> wagerFunction)
        {
            _activePlayer = _players.First;
            _activePlayer.Value.wager = wagerFunction();
            _activePlayer = _activePlayer.Next;
        }

        public void NextWager(Func<Wager> wagerFunction)
        {
            _activePlayer.Value.wager = wagerFunction();
            CheckWager(_activePlayer.Value.wager, _activePlayer.Previous.Value.wager);
            _activePlayer = _activePlayer.Next ?? _players.First;
        }

        private void CheckWager(Wager currentWager, Wager previousWager)
        {
            
        }
    }
}
