using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private CardsOrganizer cardsOrganizer = default;

        private readonly List<Card> _cards = new List<Card>();

        public IList<Card> GetAllCards() => _cards.AsReadOnly();

        public void AddCard([NotNull] Card card)
        {
            card.transform.parent = transform;
            _cards.Add(card);
            cardsOrganizer.Organize(_cards);
        }

        public void RemoveCard([NotNull] Card card)
        {
            _cards.Remove(card);
            cardsOrganizer.Organize(_cards);
        }
    }
}