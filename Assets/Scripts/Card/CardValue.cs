using System;

namespace Card
{
    public enum CardValue
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Height,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    static class CardValueMethods
    {
        public static string GetStringValue(this CardValue value)
        {
            switch (value)
            {
                case CardValue.Ace:
                    return "A";
                case CardValue.Two:
                    return "2";
                case CardValue.Three:
                    return "3";
                case CardValue.Four:
                    return "4";
                case CardValue.Five:
                    return "5";
                case CardValue.Six:
                    return "6";
                case CardValue.Seven:
                    return "7";
                case CardValue.Height:
                    return "8";
                case CardValue.Nine:
                    return "9";
                case CardValue.Ten:
                    return "10";
                case CardValue.Jack:
                    return "J";
                case CardValue.Queen:
                    return "Q";
                case CardValue.King:
                    return "K";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, "No string value.");
            }
        }
    }
}