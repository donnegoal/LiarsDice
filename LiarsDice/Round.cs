using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dice;

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
            if (currentWager.isChallenge)
            {
                DetermineWinnner(previousWager);
            }
            else CheckWagerIsValid(currentWager, previousWager);
        }

        private void CheckWagerIsValid(Wager currentWager, Wager previousWager)
        {
            const int MinFaceValue = 1;
            const int MaxFaceValue = 6;

            if (!(MaxFaceValue >= currentWager.FaceValue && currentWager.FaceValue >= MinFaceValue))
                throw new InvalidWagerException($"The face value must be between {MinFaceValue} and {MaxFaceValue}");

            if (currentWager.DiceCount <= 0 || currentWager.DiceCount > GetActiveDiceCount())
                throw new InvalidWagerException("The wager must be a positive value no greater than the number of dice in play");

            if (currentWager.DiceCount < previousWager.DiceCount)
                throw new InvalidWagerException("The player must wager a number of dice equal to or greater than the previoius wager");
            if (currentWager.DiceCount == previousWager.DiceCount)
            {
                if (currentWager.FaceValue > previousWager.FaceValue) return;
                else throw new InvalidWagerException("If the number of dice are the same, the player must wager a higher face value");
            }
            
        }

        private int GetActiveDiceCount()
        {
            var activeDiceCount = 0;
            foreach (var player in _players)
            {
                activeDiceCount += player.DiceSet.Count;
            }
            return activeDiceCount;
        }

        private void DetermineWinnner(Wager previousWager)
        {
            var allDice = GetAllDice();
            var actualCount = allDice.Where(d => d.currentValue == previousWager.FaceValue).Count();

            if (actualCount >= previousWager.DiceCount)
            {
                _winner = _activePlayer.Previous.Value;
                _loser = _activePlayer.Value;
            }
            else
            {
                _winner = _activePlayer.Value;
                _loser = _activePlayer.Previous.Value;
            }         
            
        }

        private List<StandardDie> GetAllDice()
        {
            var allDice = new List<StandardDie>();
            foreach(var player in _players)
            {
                allDice.AddRange(player.DiceSet.Dice);
            }
            return allDice;
        }
    }
}
