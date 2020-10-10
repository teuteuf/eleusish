using System;
using UnityEngine;

namespace Game.CardComponents
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10.0f;
        
        public CardValue Value { get; private set; }
        
        private CardVisual _cardVisual;
        private Vector3? _targetPosition;

        private void Awake()
        {
            _cardVisual = GetComponent<CardVisual>();
        }

        private void Update()
        {
            if (_targetPosition != null)
            {
                var transformPosition = transform.position;
                transform.position = Vector3.MoveTowards(
                    transformPosition,
                    (Vector3) _targetPosition,
                    Time.deltaTime * moveSpeed
                );

                if (transform.position == _targetPosition)
                {
                    _targetPosition = null;
                }
            }
        }

        public void SetValue(CardValue value)
        {
            Value = value;
            _cardVisual.Set(value);
        }

        public void Move(Vector3 position, Quaternion rotation)
        {
            var cardTransform = transform;
            _targetPosition = position;
            cardTransform.rotation = rotation;
        }
    }
}