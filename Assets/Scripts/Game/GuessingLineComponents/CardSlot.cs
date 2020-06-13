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
            var cardTransform = card.transform;
            
            cardTransform.parent = currentCardSlotTransform;
            
            card.Move(currentCardSlotTransform.position, currentCardSlotTransform.rotation);

            ValidCard = card;
        }
    }
}