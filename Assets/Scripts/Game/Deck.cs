using System;
using System.Collections.Generic;
using System.Linq;
using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Card prefabCard = default;
        
        private List<CardValue> _remainingCards;

        private void Awake()
        {
            ResetRemainingCards();
        }

        [CanBeNull]
        public Card DrawCard()
        {
            if (_remainingCards.Count == 0)
            {
                Debug.LogWarning("Try to draw but no more card!");
                return null;
            }
            
            var cardIndex = Random.Range(0, _remainingCards.Count);
            var cardValue = _remainingCards[cardIndex];
            _remainingCards.RemoveAt(cardIndex);

            var card = Instantiate(prefabCard, transform.position, transform.rotation);
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