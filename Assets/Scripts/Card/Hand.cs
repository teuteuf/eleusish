using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Deck deck = default;

        private List<Card> _cards = new List<Card>();

        private void Start()
        {
            var card = deck.DrawCard();
            card.transform.position = transform.position;
            
            _cards.Add(card);
        }
    }
}