using Game.CardComponents;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ObjectSelector : MonoBehaviour
    {
        private const string TagTable = "Table";

        [SerializeField] private float dragDelay = 0.25f;
        [SerializeField] private float dragDistance = 20;

        private Camera _camera;
        private GameManager _gameManager;

        private float _timeClickDown;
        private Vector3 _positionClickDown;
        private GameObject _draggedGameObject;
        private Vector3 _startDraggedGameObjectScreenPoint;
        private Vector3 _lastDraggingWorldPoint;
        private Vector3 _draggingOffset;

        private void Awake()
        {
            _camera = Camera.main;
            _gameManager = GetComponent<GameManager>();
        }


        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _timeClickDown = Time.time;
                _positionClickDown = Input.mousePosition;
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

        private void HandleStartDragging(GameObject draggedGameObject)
        {
            var draggedObjectPosition = draggedGameObject.transform.position;
            _draggedGameObject = draggedGameObject;
            _startDraggedGameObjectScreenPoint = _camera.WorldToScreenPoint(draggedObjectPosition);
            
            var currentCursorPosition = GetCurrentCursorPosition(_startDraggedGameObjectScreenPoint.z);
            _lastDraggingWorldPoint = _camera.ScreenToWorldPoint(currentCursorPosition);
            _draggingOffset = draggedObjectPosition - _camera.ScreenToWorldPoint(currentCursorPosition);
        }

        private Vector3 GetCurrentCursorPosition(float zValue)
        {
            return new Vector3(Input.mousePosition.x, Input.mousePosition.y, zValue);
        }

        private void HandleDraggedGameObject(GameObject draggedGameObject)
        {
            if (!Input.GetMouseButton(0) || !draggedGameObject)
            {
                return;
            }

            var currentCursorPos = GetCurrentCursorPosition(_startDraggedGameObjectScreenPoint.z);
            var cursorWorldPosition = _camera.ScreenToWorldPoint(currentCursorPos);
            
            var relativeOffset = cursorWorldPosition  - _lastDraggingWorldPoint;
            var worldOffset = cursorWorldPosition + _draggingOffset - draggedGameObject.transform.position;

            var card = draggedGameObject.GetComponentInParent<Card>();
            if (card)
            {
                _gameManager.DragCard(card, worldOffset, relativeOffset);
            }

            if (draggedGameObject.CompareTag(TagTable))
            {
                _gameManager.DragTable(worldOffset);
            }

            _lastDraggingWorldPoint = cursorWorldPosition;
        }

        [CanBeNull]
        private GameObject GetGameObjectSelectedByClick()
        {
            if (
                !_camera
                || !Input.GetMouseButtonUp(0)
                || Time.time - _timeClickDown > dragDelay
                || Vector3.Distance(_positionClickDown, Input.mousePosition) > dragDistance
            )
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