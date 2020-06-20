using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

namespace Game.GuessingLineComponents
{
    public class GuessingLine : MonoBehaviour
    {
        [SerializeField] private CardSlot prefabCardSlot = default;
        
        [SerializeField] private float spaceBetweenCardSlots = 0.5f;
        [SerializeField] private float horizontalViewportPadding = 0.15f;

        private Camera _camera;
        private CardSlot _currentCardSlot;
        private readonly List<CardSlot> _filledCardSlots = new List<CardSlot>();

        private void Awake()
        {
            _camera = Camera.main;
            
            if (!_currentCardSlot)
            {
                var originTransform = transform;
                _currentCardSlot = Instantiate(prefabCardSlot, originTransform.position, originTransform.rotation, originTransform);
            }
        }

        public IList<Card> GetAllValidCards()
        {
            return _filledCardSlots.ConvertAll(cardSlot => cardSlot.ValidCard);
        }

        public void AddCard(Card card, bool validCard)
        {
            var currentCardSlotTransform = _currentCardSlot.transform;
            var currentCardSlotPosition = currentCardSlotTransform.position;
            var currentCardSlotRotation = currentCardSlotTransform.rotation;

            _currentCardSlot.PlaceCard(card, validCard);

            if (validCard)
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
                
                var viewportCardPosition = _camera.WorldToViewportPoint(card.transform.position);
                if (1 - viewportCardPosition.x < horizontalViewportPadding)
                {
                    transform.parent.position -= Vector3.right * spaceBetweenCardSlots;
                }
            }
        }
    }
}
