using System.Collections.Generic;
using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenCards = 0.5f;
        
        private List<Card> _cards = new List<Card>();

        public void AddCard([NotNull] Card card)
        {
            _cards.Add(card);
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