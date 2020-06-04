using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenCards = 0.5f;
        
        [SerializeField] private Deck deck = default;

        private List<Card> _cards = new List<Card>();

        public void AddCard()
        {
            var card = deck.DrawCard();
            if (card != null)
            {
                _cards.Add(card);
            }

            OrganizeCards();
        }

        private void OrganizeCards()
        {
            var handPosition = transform.position;
            var width = spaceBetweenCards * (_cards.Count - 1);
            for (var i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.position = new Vector3
                (
                    handPosition.x - width/2 + spaceBetweenCards * i,
                    handPosition.y + 0.001f * i,
                    handPosition.z
                );
            }
        }
    }
}