using System.Linq;
using Game.CardComponents;
using Game.GuessingLineComponents;
using Game.Rules;
using Menu;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneSwitcher sceneSwitcher = default;
        [SerializeField] private GameSave gameSave = default;
        [SerializeField] private Deck deck = default;
        [SerializeField] private Hand hand = default;
        [SerializeField] private GuessingLine guessingLine = default;
        [SerializeField] private DeclinedCardLine declinedCardLine = default;
        [SerializeField] private CameraManager cameraManager = default;
        [SerializeField] private ProgressSave progressSave = default;

        [SerializeField] private int startHandSize = 3;
        [SerializeField] private int nbDrawCardOnInvalidCard = 2;
        [SerializeField] private int nbDrawCardOnWrongNoPlay = 5;
        [SerializeField] private int handReductionOnGoodNoPlay = 4;

        private AbstractRule _activeRule;

        private int _nbActions = 0;
        private int _nbErrors = 0;

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
            _nbActions++;
            declinedCardLine.PutBackCards();

            var isValidCard = _activeRule.IsValid(
                guessingLine.GetAllValidCards().Select(validCard => validCard.Value).ToArray(),
                card.Value
            );

            MoveCardFromHandToGuessingLine(card, isValidCard);

            if (!isValidCard)
            {
                _nbErrors++;
                DrawCardsToHand(nbDrawCardOnInvalidCard);
            }
            else if (hand.GetAllCards().Count == 0)
            {
                HandleSuccess();
            }
        }

        public void ChooseNoPlay()
        {
            _nbActions++;
            declinedCardLine.PutBackCards();

            var allCardsInHand = hand.GetAllCards();
            var playableCards = allCardsInHand.Where(card => _activeRule.IsValid(
                    guessingLine.GetAllValidCards().Select(validCard => validCard.Value).ToArray(),
                    card.Value
                ))
                .ToList();

            var nbPlayableCards = playableCards.Count;
            var nbCardsInHand = allCardsInHand.Count;

            if (nbPlayableCards > 0)
            {
                _nbErrors++;
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

                var newNbCardsInHand = nbCardsInHand - handReductionOnGoodNoPlay;
                if (newNbCardsInHand <= 0)
                {
                    HandleSuccess();
                }
                else
                {
                    DrawCardsToHand(newNbCardsInHand);
                }
            }
        }

        public void DragCard(Card card, Vector3 worldOffset, Vector3 relativeOffset)
        {
            if (card.transform.IsChildOf(hand.transform))
            {
                hand.DragHand(relativeOffset);
            }
            else if (card.transform.IsChildOf(declinedCardLine.transform))
            {
                declinedCardLine.DragLine(relativeOffset);
            }
            else if (card.transform.IsChildOf(guessingLine.transform))
            {
                DragPlayground(worldOffset);
            }
        }

        public void DragTable(Vector3 worldOffset)
        {
            DragPlayground(worldOffset);
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
                else
                {
                    HandleFail();
                }
            }
        }

        private void DragPlayground(Vector3 offset)
        {
            cameraManager.Move(Vector3.left * offset.x, true);
        }

        private void HandleFail()
        {
            gameSave.Save(GameSave.SaveKey.LastNbActions, _nbActions);
            gameSave.Save(GameSave.SaveKey.LastNbErrors, _nbErrors);
            
            if (gameSave.HasKey(GameSave.SaveKey.ValidationRun) && gameSave.LoadBool(GameSave.SaveKey.ValidationRun))
            {
                sceneSwitcher.SwitchToValidate();
            }
            else
            {
                sceneSwitcher.SwitchToFail();
            }
        }

        private void HandleSuccess()
        {
            gameSave.Save(GameSave.SaveKey.LastNbActions, _nbActions);
            gameSave.Save(GameSave.SaveKey.LastNbErrors, _nbErrors);

            if (gameSave.HasKey(GameSave.SaveKey.ValidationRun) && gameSave.LoadBool(GameSave.SaveKey.ValidationRun))
            {
                sceneSwitcher.SwitchToValidate();
            }
            else
            {
                progressSave.UpdateRuleProgress(
                    gameSave.LoadString(GameSave.SaveKey.SelectedRule),
                    _nbErrors == 0
                        ? RuleProgress.PerfectSuccess
                        : RuleProgress.SuccessWithError
                );
                sceneSwitcher.SwitchToSuccess();
            }
        }
    }
}