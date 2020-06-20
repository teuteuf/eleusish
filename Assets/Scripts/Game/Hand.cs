using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenCards = 0.5f;
        [SerializeField] private float randomRotationAngle = 1.0f;
        
        private readonly List<Card> _cards = new List<Card>();

        public IList<Card> GetAllCards() => _cards.AsReadOnly();

        public void AddCard([NotNull] Card card)
        {
            card.transform.parent = transform;
            _cards.Add(card);
            OrganizeCards();
        }

        public void RemoveCard([NotNull] Card card)
        {
            _cards.Remove(card);
            OrganizeCards();
        }

        private void OrganizeCards()
        {
            var handPosition = transform.position;
            var width = spaceBetweenCards * (_cards.Count - 1);
            for (var i = 0; i < _cards.Count; i++)
            {
                _cards[i].Move(
                    new Vector3
                    (
                        handPosition.x - width/2 + spaceBetweenCards * i,
                        handPosition.y + 0.001f * i,
                        handPosition.z
                    ),
                    transform.rotation * GetRandomCardRotation()
                );
            }
        }
        
        private Quaternion GetRandomCardRotation()
        {
            return Quaternion.AngleAxis
            (
                Random.Range(-randomRotationAngle, randomRotationAngle),
                Vector3.up
            );
        }
    }
}