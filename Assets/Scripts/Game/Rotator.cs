using System;
using UnityEngine;

namespace Game
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 720.0f;
        
        private Quaternion? _targetRotation;

        private void Update()
        {
            if (_targetRotation != null)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    (Quaternion) _targetRotation,
                    TimeManager.instance.GetDeltaTime() * rotationSpeed
                );

                if (transform.rotation == _targetRotation)
                {
                    _targetRotation = null;
                }
            }
        }

        public void Rotate(Quaternion targetRotation)
        {
            _targetRotation = targetRotation;
        }
    }
}