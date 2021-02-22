using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;
using JetBrains.Annotations;
using Menu.Screens.GameScreen;
using UnityEngine;

namespace Game
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private CardsOrganizer cardsOrganizer = default;
        [SerializeField] private HandCardsCounter handCardsCounter = default;

        private readonly List<Card> _cards = new List<Card>();

        public IList<Card> GetAllCards() => _cards.AsReadOnly();

        public void AddCard([NotNull] Card card)
        {
            card.transform.parent = transform;
            _cards.Add(card);
            cardsOrganizer.Organize(_cards);
            
            handCardsCounter.UpdateCardCounter(_cards.Count);
        }

        public void RemoveCard([NotNull] Card card)
        {
            _cards.Remove(card);
            cardsOrganizer.Organize(_cards);
            
            handCardsCounter.UpdateCardCounter(_cards.Count);
        }

        public void DragHand(Vector3 offset)
        {
            transform.position += Vector3.right * offset.x;
            cardsOrganizer.Organize(_cards,false);
        }
    }
}