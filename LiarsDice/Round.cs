using System;
using System.Collections.Generic;
using System.Linq;
using Dice;

namespace LiarsDice
{
    class Round
    {
        private LinkedList<Player> _players;
        private LinkedListNode<Player> _activePlayer;
        public LinkedListNode<Player> previousPlayer
        { get { return _activePlayer.Previous ?? _players.Last; } }
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
            CheckWager(_activePlayer.Value.wager, previousPlayer.Value.wager);
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

        public bool IsOver()
        {
            return (_winner != null);
        }
    }
}
