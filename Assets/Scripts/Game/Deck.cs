using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public IList<CardValue> GetAllRemainingCards() => _remainingCards.AsReadOnly();

        [CanBeNull]
        public Card DrawCard()
        {
            if (_remainingCards.Count == 0)
            {
                return null;
            }

            return PickCardAtIndex(0);
        }

        [CanBeNull]
        public Card PickCard(CardValue value)
        {
            var pickedCardIndex = _remainingCards.FindIndex(cardValue => cardValue.Rank == value.Rank && cardValue.Suite == value.Suite);
            if (pickedCardIndex == -1)
            {
                return null;
            }

            return PickCardAtIndex(pickedCardIndex);
        }

        private Card PickCardAtIndex(int cardIndex)
        {
            var cardValue = _remainingCards[cardIndex];
            _remainingCards.RemoveAt(cardIndex);

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