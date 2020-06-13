using Game.CardComponents;
using UnityEngine;

namespace Game.GuessingLineComponents
{
    public class CardSlot : MonoBehaviour
    {
        public Card ValidCard { get; private set; } = default;

        public void PlaceCard(Card card, bool respectRule)
        {
            var currentCardSlotTransform = transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var currentCardSlotRotation = currentCardSlotTransform.rotation;
            var cardTransform = card.transform;
            
            cardTransform.parent = currentCardSlotTransform;
            cardTransform.position = currentCardSlotPosition;
            cardTransform.rotation = currentCardSlotRotation;

            ValidCard = card;
        }
    }
}