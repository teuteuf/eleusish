using System.Linq;
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
        [SerializeField] private int nbDrawCardOnInvalidCard = 2;
        
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
                for (var i = 0; i < nbDrawCardOnInvalidCard; i++)
                {
                    DrawCardToHand();
                }
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

        public void DrawCardToHand()
        {
            var card = deck.DrawCard();
            if (card)
            {
                hand.AddCard(card);
            }
        }

        private void DragPlayground(Vector3 offset)
        {
            guessingLine.transform.parent.position += Vector3.right * offset.x;
        }
    }
}