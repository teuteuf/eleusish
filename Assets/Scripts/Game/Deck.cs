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

        [SerializeField] private int nbCardsDuplicate = 2;

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
                return null;
            }

            var cardValue = _remainingCards[0];
            _remainingCards.RemoveAt(0);

            var deckTransform = transform;
            var card = Instantiate(prefabCard, deckTransform.position, deckTransform.rotation);
            card.SetValue(cardValue);

            return card;
        }

        private void ResetRemainingCards()
        {
            var allCards = new List<CardValue>();
            
            foreach (var suite in Enum.GetValues(typeof(CardSuite)).Cast<CardSuite>())
            {
                foreach (var rank in Enum.GetValues(typeof(CardRank)).Cast<CardRank>())
                {
                    for (var i = 0; i < nbCardsDuplicate; i++)
                    {
                        allCards.Add(new CardValue {Rank = rank, Suite = suite});
                    }
                }
            }

            _remainingCards = new List<CardValue>();
            while (allCards.Count > 0)
            {
                var cardIndex = Random.Range(0, allCards.Count);
                _remainingCards.Add(allCards[cardIndex]);
                allCards.RemoveAt(cardIndex);
            }
        }
    }
}