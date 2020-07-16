using System.Collections.Generic;
using Game.CardComponents;
using Game.GuessingLineComponents;
using UnityEngine;

namespace Game
{
    public class DeclinedCardLine : MonoBehaviour
    {
        [SerializeField] private CardsOrganizer cardsOrganizer = default;
        
        private CardSlot _shownCardSlot;
        private List<Card> _declinedCards;

        public void ShowCardSlotContent(CardSlot cardSlot)
        {
            PutBackCards();
            
            var invalidCards = cardSlot.WithdrawInvalidCards();

            invalidCards.ForEach(card => card.transform.parent = transform);
            cardsOrganizer.Organize(invalidCards);

            _declinedCards = invalidCards;
            _shownCardSlot = cardSlot;
        }

        public void PutBackCards()
        {
            if (_shownCardSlot)
            {
                _declinedCards.ForEach(card => _shownCardSlot.PlaceCard(card, false));
                
                _declinedCards = null;
                _shownCardSlot = null;
            }
        }
    }
}
