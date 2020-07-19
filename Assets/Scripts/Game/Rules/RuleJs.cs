using System;
using System.Collections.Generic;
using System.Linq;
using Game.CardComponents;
using JetBrains.Annotations;
using Jint;
using Jint.Native;
using UnityEngine;

namespace Game.Rules
{
    public class RuleJs : AbstractRule
    {
        [TextArea(10, 20)]
        [SerializeField]
        private string jsRule = default;
        
        private Engine _jintEngine;
        private JsValue _getInitialCards;
        private JsValue _isValid;

        private void Awake()
        {
            _jintEngine = new Engine();

            _jintEngine.Execute(jsRule);

            _getInitialCards = _jintEngine.GetValue("getInitialCards");
            _isValid = _jintEngine.GetValue("isValid");
        }

        public override IList<CardValue> GetInitialCardValues(IList<CardValue> remainingCards)
        {
            var remainingCardsForJs = remainingCards.Select(SerializeCardValueForJs).ToArray();

            var initialCardValuesForJs = _getInitialCards
                .Invoke(JsValue.FromObject(_jintEngine, remainingCardsForJs)).AsArray();

            var initialCardValues = new List<CardValue>();
            foreach (var value in initialCardValuesForJs)
            {
                initialCardValues.Add(
                    new CardValue
                    {
                        Rank = (CardRank) Enum.Parse(typeof(CardRank), value.Get("Rank").AsString()),
                        Suite = (CardSuite) Enum.Parse(typeof(CardSuite), value.Get("Suite").AsString())
                    }
                );
            }

            return initialCardValues;
        }

        public override bool IsValid(IList<CardValue> previousCards, CardValue newCard)
        {
            var previousCardsForJs = previousCards.Select(SerializeCardValueForJs).ToArray();
            var newCardForJs = SerializeCardValueForJs(newCard);

            return _isValid.Invoke(
                JsValue.FromObject(_jintEngine, previousCardsForJs),
                JsValue.FromObject(_jintEngine, newCardForJs)
            ).AsBoolean();
        }


        private CardValueForJs SerializeCardValueForJs(CardValue cardValue)
        {
            return new CardValueForJs
            {
                Rank = cardValue.Rank.ToString(),
                Suite = cardValue.Suite.ToString()
            };
        }

        private struct CardValueForJs
        {
            public string Rank { [UsedImplicitly] get; set; }
            public string Suite { [UsedImplicitly] get; set; }
        }
    }
}