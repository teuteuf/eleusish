using System;
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

        private Vector3 _originalLinePosition;

        private void Awake()
        {
            _originalLinePosition = transform.position;
        }

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
                transform.position = _originalLinePosition;

                _declinedCards.ForEach(card => _shownCardSlot.PlaceCard(card, false));

                _declinedCards = null;
                _shownCardSlot = null;
            }
        }

        public void DragLine(Vector3 offset)
        {
           transform.position += Vector3.right * offset.x;
           cardsOrganizer.Organize(_declinedCards,false);
        }
    }
}