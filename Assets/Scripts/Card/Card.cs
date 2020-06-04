using System;
using UnityEngine;

namespace Card
{
    public class Card : MonoBehaviour
    {
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
    }
}
