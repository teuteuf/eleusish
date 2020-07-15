using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class ObjectSelector : MonoBehaviour
    {
        private const string TagTable = "Table";
        
        [SerializeField] private float dragDelay = 0.25f;

        private Camera _camera;
        private GameManager _gameManager;

        private float _timeClickDown;
        private GameObject _draggedGameObject;
        private Vector3 _startDraggingScreenPoint;
        private Vector3 _draggingOffset;

        private void Awake()
        {
            _camera = Camera.main;
            _gameManager = GetComponent<GameManager>();
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _timeClickDown = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _draggedGameObject = null;
            }

            var selectedGameObject = GetGameObjectSelectedByClick();
            HandleGameObjectSelection(selectedGameObject);

            var startDraggingGameObject = GetStartDraggingGameObject();
            if (startDraggingGameObject)
            {
                HandleStartDragging(startDraggingGameObject);
            }

            HandleDraggedGameObject(_draggedGameObject);
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
                _gameManager.SelectCard(card);
            }
        }

        private void HandleStartDragging(GameObject startDraggingGameObject)
        {
            _draggedGameObject = startDraggingGameObject;

            var draggedPosition = startDraggingGameObject.transform.position;

            _startDraggingScreenPoint = _camera.WorldToScreenPoint(draggedPosition);

            var cursorPosition = GetCurrentCursorPosition();
            _draggingOffset = draggedPosition - _camera.ScreenToWorldPoint(cursorPosition);
        }

        private Vector3 GetCurrentCursorPosition()
        {
            return new Vector3(Input.mousePosition.x, Input.mousePosition.y, _startDraggingScreenPoint.z);
        }

        private void HandleDraggedGameObject(GameObject draggedGameObject)
        {
            if (!Input.GetMouseButton(0) || !draggedGameObject)
            {
                return;
            }
            
            var currentCursorPos = GetCurrentCursorPosition();
            var cursorWorldPosition = _camera.ScreenToWorldPoint(currentCursorPos) + _draggingOffset;
            var offset = cursorWorldPosition - draggedGameObject.transform.position;
            
            var card = draggedGameObject.GetComponentInParent<Card>();
            if (card)
            {
                _gameManager.DragCard(card, offset);
            }

            if (draggedGameObject.CompareTag(TagTable))
            {
                _gameManager.DragTable(offset);
            }
        }

        [CanBeNull]
        private GameObject GetGameObjectSelectedByClick()
        {
            if (!_camera || !Input.GetMouseButtonUp(0) || Time.time - _timeClickDown > dragDelay)
            {
                return null;
            }

            return GetGameObjectOnCursor();
        }

        [CanBeNull]
        private GameObject GetStartDraggingGameObject()
        {
            if (!_camera || !Input.GetMouseButtonDown(0))
            {
                return null;
            }

            return GetGameObjectOnCursor();
        }

        private GameObject GetGameObjectOnCursor()
        {
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