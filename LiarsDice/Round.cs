using System;
using System.Collections.Generic;
using System.Linq;
using Dice;

namespace LiarsDice
{
    internal class Round
    {
        private readonly LinkedList<Player> _players;
        private LinkedListNode<Player> _activePlayer;
        private LinkedListNode<Player> PreviousPlayer => _activePlayer.Previous ?? _players.Last;
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
            OpeniningWager();
        }

        public void OpeniningWager()
        {
            _activePlayer = _players.First;
            _activePlayer.Value.MakeWager();
            _activePlayer = _activePlayer.Next;
        }

        internal void Close()
        {
            _loser.DiceSet.RemoveDice(1);
            if(_loser.DiceSet.Dice.Count == 0)
            {
                _players.Remove(_loser);
            }
        }

        public void NextWager()
        {
            _activePlayer.Value.MakeWager();
            CheckWager(_activePlayer.Value.Wager, PreviousPlayer.Value.Wager);
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
            const int minFaceValue = 1;
            const int maxFaceValue = 6;

            if (!(maxFaceValue >= currentWager.FaceValue && currentWager.FaceValue >= minFaceValue))
                throw new InvalidWagerException($"The face value must be between {minFaceValue} and {maxFaceValue}");

            if (currentWager.DiceCount <= 0 || currentWager.DiceCount > GetActiveDiceCount())
                throw new InvalidWagerException("The wager must be a positive value no greater than the number of dice in play");

            if (currentWager.DiceCount < previousWager.DiceCount)
                throw new InvalidWagerException("The player must wager a number of dice equal to or greater than the previoius wager");
            if (currentWager.DiceCount != previousWager.DiceCount) return;
            if (currentWager.FaceValue > previousWager.FaceValue) return;
            throw new InvalidWagerException("If the number of dice are the same, the player must wager a higher face value");
        }

        private int GetActiveDiceCount()
        {
            return _players.Sum(player => player.DiceSet.Count);
        }

        private void DetermineWinnner(Wager previousWager)
        {
            var allDice = GetAllDice();
            var actualCount = allDice.Count(d => d.currentValue == previousWager.FaceValue);

            if (actualCount >= previousWager.DiceCount)
            {
                if (_activePlayer.Previous != null) _winner = _activePlayer.Previous.Value;
                _loser = _activePlayer.Value;
            }
            else
            {
                _winner = _activePlayer.Value;
                if (_activePlayer.Previous != null) _loser = _activePlayer.Previous.Value;
            }         
            
        }

        private IEnumerable<StandardDie> GetAllDice()
        {
            var allDice = new List<StandardDie>();
            foreach(var player in _players)
            {
                allDice.AddRange(player.DiceSet.Dice);
            }
            return allDice;
        }

        public bool IsOver()
        {
            return (_winner != null);
        }
    }
}
