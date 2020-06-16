using System;
using Game.CardComponents;
using Game.GuessingLineComponents;
using Game.Rules;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Deck deck = default;
        [SerializeField] private Hand hand = default;
        [SerializeField] private GuessingLine guessingLine = default;

        [SerializeField] private int startHandSize = 3;
        
        private RuleBlackAndRed _activeRule;

        private void Awake()
        {
            _activeRule = GetComponent<RuleBlackAndRed>();
        }

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

            var isValidCard = _activeRule.IsValid(guessingLine.GetAllValidCards(), card);
            guessingLine.AddCard(card, isValidCard);

            if (!isValidCard)
            {
                DrawCardToHand();
                DrawCardToHand();
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
                guessingLine.transform.parent.position += Vector3.right * offset.x;
            }
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