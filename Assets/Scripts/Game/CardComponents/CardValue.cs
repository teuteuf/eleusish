using System;

namespace Game.CardComponents
{
    [Serializable]
    public struct CardValue
    {
        public CardRank Rank;
        public CardSuite Suite;
    }
}