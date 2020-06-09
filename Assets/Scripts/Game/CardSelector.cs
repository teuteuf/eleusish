using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class CardSelector : MonoBehaviour
    {
        private Camera _camera;
        private GameManager _gameManager;
        
        private void Awake()
        {
            _camera = Camera.main;
            _gameManager = GetComponent<GameManager>();
        }


        private void Update()
        {
            var selectedGameObject = GetGameObjectSelectedByClick();
            HandleGameObjectSelection(selectedGameObject);
        }

        private void HandleGameObjectSelection(GameObject selectedGameObject)
        {
            if (!selectedGameObject)
            {
                return;
            }
            
            var card = selectedGameObject.GetComponentInParent<Card>();
            if (card)
            {
                _gameManager.PlayCard(card);
            }
        }

        [CanBeNull]
        private GameObject GetGameObjectSelectedByClick()
        {
            if (!_camera || !Input.GetMouseButtonUp(0))
            {
                return null;
            }
            
            GameObject selectedGameObject = null;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                selectedGameObject = hit.collider.gameObject;
            }

            return selectedGameObject;
        }
    }
}
