using System;
using UnityEngine;

namespace Game.CardComponents
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Mover mover = default;
        
        [SerializeField] private float rotationSpeed = 180.0f;
        
        public CardValue Value { get; private set; }
        
        private CardVisual _cardVisual;
        private Quaternion? _targetRotation;

        private void Awake()
        {
            _cardVisual = GetComponent<CardVisual>();
        }

        private void Update()
        {
            if (_targetRotation != null)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    (Quaternion) _targetRotation,
                    Time.deltaTime * rotationSpeed
                );

                if (transform.rotation == _targetRotation)
                {
                    _targetRotation = null;
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
            mover.Move(position, false, null);
            _targetRotation = rotation;
        }
    }
}