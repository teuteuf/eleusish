using System;
using UnityEngine;

namespace Game.CardComponents
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10.0f;
        [SerializeField] private float maxMoveDuration = 0.5f;
        
        public CardValue Value { get; private set; }
        
        private CardVisual _cardVisual;
        private Vector3? _targetPosition;
        private float _actualMoveSpeed;

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
                    Time.deltaTime * _actualMoveSpeed
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

            var distance = Vector3.Distance(cardTransform.position, position);
            var speedForMaxMoveDuration = distance / maxMoveDuration;
            
            _actualMoveSpeed = Mathf.Max(moveSpeed, speedForMaxMoveDuration);

            _targetPosition = position;
            cardTransform.rotation = rotation;
        }
    }
}