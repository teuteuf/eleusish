using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10.0f;
        [SerializeField] private float maxMoveDuration = 0.75f;

        private Vector3? _targetPosition;
        private float _actualMoveSpeed;
        private Action _onMove;
        
        private void Update()
        {
            if (_targetPosition == null)
            {
                return;
            }
            
            transform.position = Vector3.MoveTowards(
                transform.position,
                (Vector3) _targetPosition,
                TimeManager.instance.GetDeltaTime() * _actualMoveSpeed
            );

            _onMove?.Invoke();

            if (transform.position == _targetPosition)
            {
                _targetPosition = null;
            }
        }

        public void Move(Vector3 position, bool instant, [CanBeNull] Action onMove)
        {
            if (instant)
            {
                _targetPosition = null;
                transform.position = position;
                onMove?.Invoke();
            }
            else 
            {
                var distance = Vector3.Distance(transform.position, position);
                var speedForMaxMoveDuration = distance / maxMoveDuration;
            
                _actualMoveSpeed = Mathf.Max(moveSpeed, speedForMaxMoveDuration);
                _targetPosition = position;
                _onMove = onMove;
            }
        }
    }
}