using System;
using Game.CardComponents;
using Game.GuessingLineComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Deck deck = default;
        [SerializeField] private Hand hand = default;
        [SerializeField] private GuessingLine guessingLine = default;

        [SerializeField] private int startHandSize = 3;

        private void Start()
        {
            for (var i = 0; i < startHandSize; i++)
            {
                DrawCardToHand();
            }
        }

        public void PlayCard(Card card)
        {
            if (card.transform.parent != hand.transform)
            {
                return;
            }
            
            hand.RemoveCard(card);
            guessingLine.AddCard(card);
            DrawCardToHand();
        }

        public void DrawCardToHand()
        {
            var card = deck.DrawCard();
            if (card)
            {
                hand.AddCard(card);
            }
        }
    }
}