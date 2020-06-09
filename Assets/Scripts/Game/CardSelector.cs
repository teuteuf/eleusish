using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class CardSelector : MonoBehaviour
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }


        private void Update()
        {
            var selectedGameObject = GetGameObjectSelectedByClick();
            HandleGameObjectSelection(selectedGameObject);
        }

        private static void HandleGameObjectSelection(GameObject selectedGameObject)
        {
            if (!selectedGameObject)
            {
                return;
            }
            
            var card = selectedGameObject.GetComponentInParent<Card>();
            if (card)
            {
                Debug.Log($"{card.Value.Suite} {card.Value.Rank}");
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
