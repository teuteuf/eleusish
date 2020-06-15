using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

namespace Game.GuessingLineComponents
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenInvalidCards = 0.5f;
        [SerializeField] private float spaceBetweenInvalidAndValidCards = 0.5f;
        
        public Card ValidCard { get; private set; } = default;
        
        private List<Card> _invalidCards = new List<Card>();

        public void PlaceCard(Card card, bool respectRule)
        {
            if (respectRule)
            {
                ValidCard = card;
                PositionValidCard(card);
            }
            else
            {
                _invalidCards.Add(card);
                PositionInvalidCard(card);
            }
        }

        private void PositionValidCard(Card card)
        {
            var currentCardSlotTransform = transform;
            var cardTransform = card.transform;

            cardTransform.parent = currentCardSlotTransform;

            card.Move(currentCardSlotTransform.position, currentCardSlotTransform.rotation);
        }

        private void PositionInvalidCard(Card card)
        {
            var currentCardSlotTransform = transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var cardTransform = card.transform;

            var cardRotation = currentCardSlotTransform.rotation * Quaternion.AngleAxis(180.0f, Vector3.forward);
            var cardPosition = new Vector3(
                currentCardSlotPosition.x,
                currentCardSlotPosition.y + _invalidCards.Count * 0.001f,
                currentCardSlotPosition.z + spaceBetweenInvalidAndValidCards + spaceBetweenInvalidCards * _invalidCards.Count
            );

            cardTransform.parent = currentCardSlotTransform;
            
            card.Move(cardPosition, cardRotation);
        }
    }
}