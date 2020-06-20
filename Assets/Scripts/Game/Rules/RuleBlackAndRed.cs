using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;

namespace Game.Rules
{
    public class RuleBlackAndRed : AbstractRule
    {
        public override List<CardValue> GetInitialCardValues(ReadOnlyCollection<CardValue> remainingCards)
        {
            return new List<CardValue> {remainingCards[0]};
        }

        public override bool IsValid(List<Card> previousCards, Card newCard)
        {
            var nbPreviousCards = previousCards.Count;
            if (nbPreviousCards == 0)
            {
                return true;
            }

            var lastCard = previousCards[nbPreviousCards - 1];
            var lastSuite = lastCard.Value.Suite;
            var newSuite = newCard.Value.Suite;

            return IsRed(lastSuite) && IsBlack(newSuite) || IsBlack(lastSuite) && IsRed(newSuite);
        }

        private bool IsRed(CardSuite suite)
        {
            return suite == CardSuite.Diamonds || suite == CardSuite.Hearts;
        }

        private bool IsBlack(CardSuite suite)
        {
            return suite == CardSuite.Spades || suite == CardSuite.Clubs;
        }
    }
}