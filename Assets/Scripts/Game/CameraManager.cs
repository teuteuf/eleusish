using System;
using System.Linq;
using Game.CardComponents;
using UnityEngine;

namespace Game
{
    public class CameraManager : MonoBehaviour
    {
        private Camera _camera;
        private CardsOrganizer[] _cardsOrganizers;

        private void Awake()
        {
            _camera = Camera.main;
            _cardsOrganizers = GetComponentsInChildren<CardsOrganizer>();
        }

        public Vector3 GetViewportPoint(Vector3 worldPosition)
        {
            return _camera.WorldToViewportPoint(worldPosition);
        }

        public void Move(Vector3 offset)
        {
            _camera.transform.position += offset;
            foreach (var cardsOrganizer in _cardsOrganizers)
            {
                cardsOrganizer.Organize(cardsOrganizer.GetComponentsInChildren<Card>().ToList(), false);
            }
        }
    }
}