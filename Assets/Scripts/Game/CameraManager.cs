using System;
using System.Linq;
using Game.CardComponents;
using UnityEngine;

namespace Game
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Mover mover = default;
        
        private Camera _camera;
        private CardsOrganizer[] _cardsOrganizers;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _cardsOrganizers = GetComponentsInChildren<CardsOrganizer>();
        }

        public Vector3 GetViewportPoint(Vector3 worldPosition)
        {
            return _camera.WorldToViewportPoint(worldPosition);
        }

        public void Move(Vector3 offset, bool instantMove)
        {
            mover.Move(
                transform.position + offset,
                instantMove,
                () =>
                {
                    foreach (var cardsOrganizer in _cardsOrganizers)
                    {
                        var cards = cardsOrganizer.GetComponentsInChildren<Card>().ToList();
                        cardsOrganizer.Organize(cards, false);
                    }
                }
            );
        }
    }
}