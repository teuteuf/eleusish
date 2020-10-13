using System;
using System.Linq;
using Game.CardComponents;
using UnityEngine;

namespace Game
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;
        
        private Camera _camera;
        private CardsOrganizer[] _cardsOrganizers;
        private Vector3? _targetPosition;

        private void Awake()
        {
            _camera = Camera.main;
            _cardsOrganizers = GetComponentsInChildren<CardsOrganizer>();
        }

        private void Update()
        {
            if (_targetPosition != null)
            {
                var currentPosition = _camera.transform.position;
                _camera.transform.position = Vector3.MoveTowards(
                    currentPosition,
                    (Vector3) _targetPosition,
                    Time.deltaTime * moveSpeed
                );
                
                
                foreach (var cardsOrganizer in _cardsOrganizers)
                {
                    var cards = cardsOrganizer.GetComponentsInChildren<Card>().ToList();
                    cardsOrganizer.Organize(cards, false);
                }

                if (currentPosition == _targetPosition)
                {
                    _targetPosition = null;
                }
            }
        }

        public Vector3 GetViewportPoint(Vector3 worldPosition)
        {
            return _camera.WorldToViewportPoint(worldPosition);
        }

        public void Move(Vector3 offset, bool instantMove)
        {
            _targetPosition = _camera.transform.position + offset;
            
            if (instantMove)
            {
                _camera.transform.position = (Vector3) _targetPosition;
            }
        }
    }
}