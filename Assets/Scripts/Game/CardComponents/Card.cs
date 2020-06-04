using UnityEngine;

namespace Game.CardComponents
{
    public class Card : MonoBehaviour
    {
        private CardVisual _cardVisual;
        public CardValue Value { get; private set; }

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