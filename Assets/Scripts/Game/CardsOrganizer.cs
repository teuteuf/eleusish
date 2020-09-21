using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;

namespace Game
{
    public class CardsOrganizer : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenCards = 0.5f;
        [SerializeField] private float randomRotationAngle = 2.0f;

        public void Organize(List<Card> cards)
        {
            var handPosition = transform.position;
            var width = spaceBetweenCards * (cards.Count - 1);
            for (var i = 0; i < cards.Count; i++)
            {
                cards[i].Move(
                    new Vector3
                    (
                        handPosition.x - width * 0.5f + spaceBetweenCards * i,
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