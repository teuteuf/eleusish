using System.Collections.Generic;
using Game.CardComponents;
using Game.GuessingLineComponents.Rules;
using UnityEngine;

namespace Game.GuessingLineComponents
{
    public class GuessingLine : MonoBehaviour
    {
        [SerializeField] private CardSlot prefabCardSlot = default;
        
        [SerializeField] private float spaceBetweenCardSlots = 0.5f;

        private CardSlot _currentCardSlot;
        private List<CardSlot> _filledCardSlots = new List<CardSlot>();

        private RuleBlackAndRed _activeRule;

        private void Awake()
        {
            _activeRule = GetComponent<RuleBlackAndRed>();

            if (!_currentCardSlot)
            {
                var originTransform = transform;
                _currentCardSlot = Instantiate(prefabCardSlot, originTransform.position, originTransform.rotation, originTransform);
            }
        }

        public void AddCard(Card card)
        {
            var currentCardSlotTransform = _currentCardSlot.transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var currentCardSlotRotation = currentCardSlotTransform.rotation;

            var isValidCard = _activeRule.IsValid(
                _filledCardSlots.ConvertAll(cardSlot => cardSlot.ValidCard),
                card
            );

            _currentCardSlot.PlaceCard(card, isValidCard);

            if (isValidCard)
            {
                _filledCardSlots.Add(_currentCardSlot);
                _currentCardSlot = Instantiate(
                    prefabCardSlot,
                    new Vector3(
                        currentCardSlotPosition.x + spaceBetweenCardSlots,
                        currentCardSlotPosition.y + 0.001f,
                        currentCardSlotPosition.z
                    ),
                    currentCardSlotRotation,
                    transform
                );
            }
        }
    }
}
