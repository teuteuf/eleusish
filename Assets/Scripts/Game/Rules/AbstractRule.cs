using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;
using UnityEngine;

namespace Game.Rules
{
    public abstract class AbstractRule : MonoBehaviour
    {
        public abstract IList<CardValue> GetInitialCardValues(IList<CardValue> remainingCards);
        public abstract bool IsValid(IList<Card> previousCards, Card newCard);
    }
}