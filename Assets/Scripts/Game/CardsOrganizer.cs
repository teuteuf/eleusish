using System.Collections.Generic;
using Game.CardComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class CardsOrganizer : MonoBehaviour
    {
        [SerializeField] private float spaceBetweenCards = 0.5f;
        [SerializeField] private float randomRotationAngle = 2.0f;
        [SerializeField] private float cardsWidth = 4.0f;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Organize(List<Card> cards, bool rotationOffset = true)
        {
            var handPosition = transform.position;
            var width = spaceBetweenCards * (cards.Count - 1);
            var cameraX = _camera.transform.position.x;
            
            for (var i = 0; i < cards.Count; i++)
            {
                var cardX = handPosition.x - width * 0.5f + spaceBetweenCards * i;
                var clampedCardX = Mathf.Clamp(
                    cardX,
                    cameraX - cardsWidth / 2,
                    cameraX + cardsWidth / 2
                );
                
                var cardSideSign = Mathf.Sign(cardX - cameraX);
                var cardRotation = transform.rotation;
                
                cards[i].Move(
                    new Vector3
                    (
                        clampedCardX,
                        handPosition.y + 0.001f * i * cardSideSign * -1,
                        handPosition.z
                    ),
                    rotationOffset ? cardRotation * GetRandomCardRotation() : cardRotation
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