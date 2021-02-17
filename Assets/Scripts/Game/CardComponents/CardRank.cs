using System;

namespace Game.CardComponents
{
    public enum CardRank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    internal static class CardRankMethods
    {
        public static string GetStringValue(this CardRank rank)
        {
            switch (rank)
            {
                case CardRank.Ace:
                    return "A";
                case CardRank.Two:
                    return "2";
                case CardRank.Three:
                    return "3";
                case CardRank.Four:
                    return "4";
                case CardRank.Five:
                    return "5";
                case CardRank.Six:
                    return "6";
                case CardRank.Seven:
                    return "7";
                case CardRank.Eight:
                    return "8";
                case CardRank.Nine:
                    return "9";
                case CardRank.Ten:
                    return "10";
                case CardRank.Jack:
                    return "J";
                case CardRank.Queen:
                    return "Q";
                case CardRank.King:
                    return "K";
                default:
                    throw new ArgumentOutOfRangeException(nameof(rank), rank, "No string rank.");
            }
        }
    }
}