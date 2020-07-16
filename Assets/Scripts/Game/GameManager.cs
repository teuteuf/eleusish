using System;
using System.Linq;
using Game.CardComponents;
using Game.GuessingLineComponents;
using Game.Rules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Deck deck = default;
        [SerializeField] private Hand hand = default;
        [SerializeField] private GuessingLine guessingLine = default;
        [SerializeField] private DeclinedCardLine declinedCardLine = default;

        [SerializeField] private int startHandSize = 3;
        [SerializeField] private int nbDrawCardOnInvalidCard = 2;
        [SerializeField] private int nbDrawCardOnWrongNoPlay = 5;
        [SerializeField] private int handReductionOnGoodNoPlay = 4;
        
        private AbstractRule _activeRule;

        private void Awake()
        {
            _activeRule = GetComponent<AbstractRule>();
        }

        private void Start()
        {
            var initialCardValues = _activeRule.GetInitialCardValues(deck.GetAllRemainingCards());
            foreach (var pickedCard in initialCardValues.Select(initialCardValue => deck.PickCard(initialCardValue)))
            {
                guessingLine.AddCard(pickedCard, true);
            }
            
            DrawCardsToHand(startHandSize);
        }

        public void SelectCard(Card card)
        {

            if (card.transform.IsChildOf(guessingLine.transform))
            {
                var cardSlot = card.GetComponentInParent<CardSlot>();
                declinedCardLine.ShowCardSlotContent(cardSlot);
            }
            else if (card.transform.IsChildOf(declinedCardLine.transform))
            {
                declinedCardLine.PutBackCards();
            }
            else if (card.transform.IsChildOf(hand.transform))
            {
                PlayCard(card);
            }
        }

        private void PlayCard(Card card)
        {
            declinedCardLine.PutBackCards();
            
            var isValidCard = _activeRule.IsValid(guessingLine.GetAllValidCards(), card);
            MoveCardFromHandToGuessingLine(card, isValidCard);

            if (!isValidCard)
            {
                DrawCardsToHand(nbDrawCardOnInvalidCard);
            }
        }

        public void ChooseNoPlay()
        {
            declinedCardLine.PutBackCards();
            
            var allCardsInHand = hand.GetAllCards();
            var playableCards = allCardsInHand.Where(card => _activeRule.IsValid(guessingLine.GetAllValidCards(), card)).ToList();

            var nbPlayableCards = playableCards.Count;
            var nbCardsInHand = allCardsInHand.Count;
            
            if (nbPlayableCards > 0)
            {
                var randomPlayableCard = playableCards[Random.Range(0, nbPlayableCards)];
                MoveCardFromHandToGuessingLine(randomPlayableCard, true);
                DrawCardsToHand(nbDrawCardOnWrongNoPlay);
            }
            else
            {
                for (var index = allCardsInHand.Count - 1; index >= 0; index--)
                {
                    var card = allCardsInHand[index];
                    MoveCardFromHandToGuessingLine(card, false);
                }

                DrawCardsToHand(Math.Max(0, nbCardsInHand - handReductionOnGoodNoPlay));
            }
        }

        public void DragCard(Card card, Vector3 offset)
        {
            if (card.transform.parent == hand.transform)
            {
                hand.transform.position += Vector3.right * offset.x;
            }
            
            if (card.transform.parent.parent == guessingLine.transform)
            {
                DragPlayground(offset);
            }
        }

        public void DragTable(Vector3 offset)
        {
            DragPlayground(offset);
        }

        private void MoveCardFromHandToGuessingLine(Card card, bool isValidCard)
        {
            hand.RemoveCard(card);
            guessingLine.AddCard(card, isValidCard);
        }

        private void DrawCardsToHand(int nbCards)
        {
            for (var i = 0; i < nbCards; i++)
            {
                var card = deck.DrawCard();
                if (card)
                {
                    hand.AddCard(card);
                }
            }
        }

        private void DragPlayground(Vector3 offset)
        {
            guessingLine.transform.parent.position += Vector3.right * offset.x;
        }
    }
}