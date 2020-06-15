using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

namespace Game.Rules
{
    public class RuleBlackAndRed : MonoBehaviour
    {
        public bool IsValid(List<Card> previousCards, Card newCard)
        {
            var nbPrviousCards = previousCards.Count;
            if (nbPrviousCards == 0)
            {
                return true;
            }

            var lastCard = previousCards[nbPrviousCards - 1];
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