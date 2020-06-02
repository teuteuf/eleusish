using System;
using UnityEngine;

namespace Card
{
    public class Card : MonoBehaviour
    {
        public CardValue Value { get; private set; }
        
        private CardVisual _cardVisual;

        private void Start()
        {
            _cardVisual = GetComponent<CardVisual>();
            SetValue(new CardValue
            {
                Rank = CardRank.Ace,
                Suite = CardSuite.Spades
            });
        }

        public void SetValue(CardValue value)
        {
            Value = value;
            _cardVisual.Set(value);
        }
    }
}
