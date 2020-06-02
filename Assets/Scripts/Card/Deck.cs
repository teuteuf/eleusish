using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Card
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Card prefabCard = default;
        
        private List<CardValue> _remainingCards;

        private void Start()
        {
            ResetRemainingCards();
        }

        public Card DrawCard()
        {
            var cardIndex = Random.Range(0, _remainingCards.Count);
            var cardValue = _remainingCards[cardIndex];
            _remainingCards.RemoveAt(cardIndex);

            var card = Instantiate(prefabCard);
            card.SetValue(cardValue);

            return card;
        }

        private void ResetRemainingCards()
        {
            _remainingCards = new List<CardValue>();
            foreach (var suite in Enum.GetValues(typeof(CardSuite)).Cast<CardSuite>())
            {
                foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
                {
                    _remainingCards.Add(new CardValue
                    {
                        Rank = rank,
                        Suite = suite
                    });
                }
            }
        }
    }
}