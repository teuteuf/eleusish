using System;
using UnityEngine;

namespace Game.CardComponents
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Mover mover = default;
        [SerializeField] private Rotator rotator = default;
        
        public CardValue Value { get; private set; }
        
        private CardVisual _cardVisual;

        private void Awake()
        {
            _cardVisual = GetComponent<CardVisual>();
        }

        public void SetValue(CardValue value)
        {
            Value = value;
            _cardVisual.Set(value);
        }

        public void Move(Vector3 position, Quaternion rotation)
        {
            mover.Move(position, false, null);
            rotator.Rotate(rotation);
        }
    }
}