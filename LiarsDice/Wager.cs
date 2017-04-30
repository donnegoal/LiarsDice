namespace LiarsDice
{
    class Wager
    {
        public int DiceCount { get; set; }
        public int FaceValue { get; set; }
        public bool isChallenge { get; set; }

        public Wager()
        {
            isChallenge = false;
        }
    }
}
