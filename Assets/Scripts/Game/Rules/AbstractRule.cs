using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;
using UnityEngine;

namespace Game.Rules
{
    public abstract class AbstractRule : MonoBehaviour
    {
        public abstract List<CardValue> GetInitialCardValues(ReadOnlyCollection<CardValue> remainingCards);
        public abstract bool IsValid(List<Card> previousCards, Card newCard);
    }
}