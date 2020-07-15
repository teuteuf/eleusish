using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

namespace Game.GuessingLineComponents
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenInvalidCards = 0.5f;
        [SerializeField] private float spaceBetweenInvalidAndValidCards = 0.5f;
        [SerializeField] private float randomRotationAngle = 5.0f;

        public Card ValidCard { get; private set; } = default;

        private readonly List<Card> _invalidCards = new List<Card>();

        private bool invalidCardsDisplayed = false;

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
                PositionInvalidCard(card, _invalidCards.Count);
            }
        }

        public void ToggleInvalidCards()
        {
            if (invalidCardsDisplayed)
            {
                HideInvalidCards();
            }
            else
            {
                ShowInvalidCards();
            }

            invalidCardsDisplayed = !invalidCardsDisplayed;
        }

        private void HideInvalidCards()
        {
            for (var i = 0; i < _invalidCards.Count; i++)
            {
                PositionInvalidCard(_invalidCards[i], i + 1);
            }
        }

        private void ShowInvalidCards()
        {
            foreach (var invalidCard in _invalidCards)
            {
                invalidCard.Move(
                    Vector3.zero,
                    transform.rotation
                );
            }
        }

        private void PositionValidCard(Card card)
        {
            var currentCardSlotTransform = transform;
            var cardTransform = card.transform;

            cardTransform.parent = currentCardSlotTransform;


            card.Move(currentCardSlotTransform.position, currentCardSlotTransform.rotation * GetRandomCardRotation());
        }

        private void PositionInvalidCard(Card card, int nbOffset)
        {
            var currentCardSlotTransform = transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var cardTransform = card.transform;

            var cardRotation = currentCardSlotTransform.rotation * Quaternion.AngleAxis(180.0f, Vector3.forward);
            var cardPosition = new Vector3(
                currentCardSlotPosition.x,
                currentCardSlotPosition.y + nbOffset * 0.001f,
                currentCardSlotPosition.z + spaceBetweenInvalidAndValidCards + spaceBetweenInvalidCards * nbOffset
            );

            cardTransform.parent = currentCardSlotTransform;

            card.Move(cardPosition, cardRotation * GetRandomCardRotation());
        }

        private Quaternion GetRandomCardRotation()
        {
            return Quaternion.AngleAxis
            (
                Random.Range(-randomRotationAngle, randomRotationAngle),
                Vector3.up
            );
        }
    }
}