using System;

namespace Game.Card
{
    [Serializable]
    public struct CardValue
    {
        public CardRank Rank;
        public CardSuite Suite;
    }
}