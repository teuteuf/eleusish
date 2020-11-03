using System;
using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GuessingLineComponents
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private ParticleSystem discardedParticles = default;

        [SerializeField] private float spaceBetweenInvalidCards = 0.5f;
        [SerializeField] private Vector2 spaceBetweenInvalidAndValidCards = new Vector2(0.0F, 1.0f);
        [SerializeField] private float randomRotationAngle = 5.0f;

        public Card ValidCard { get; private set; }

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
                PositionInvalidCard(card, _invalidCards.Count);
            }
        }

        public List<Card> WithdrawInvalidCards()
        {
            discardedParticles.Play();
            
            var withdrawnCards = _invalidCards;
            _invalidCards = new List<Card>();

            return withdrawnCards;
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
            discardedParticles.Stop();
            
            var currentCardSlotTransform = transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var cardTransform = card.transform;

            var cardRotation = currentCardSlotTransform.rotation * Quaternion.AngleAxis(180.0f, Vector3.forward);
            var cardPosition = new Vector3(
                currentCardSlotPosition.x + spaceBetweenInvalidAndValidCards.x,
                currentCardSlotPosition.y + nbOffset * 0.001f,
                currentCardSlotPosition.z + spaceBetweenInvalidAndValidCards.y + spaceBetweenInvalidCards * nbOffset
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