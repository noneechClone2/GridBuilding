using System;
using GridBuilding.Buildings;
using InputHandlers;
using InputHandlers.PlayerInputState;
using UnityEngine;

namespace _Scripts.GridBuilding.View
{
    public class CanvasRaycaster : MonoBehaviour, IDisposable
    {
        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private Camera _canvasCamera;

        private CurrentPlayerInputState _currentPlayerInputState;
        private IInputHandler _inputHandler;
        private EditingButtons _editingButtons;
        private Building _building;

        private Vector2 _canvasBuildingPosition;
        private Vector3 _positionOnMainCamera;
        private Ray _ray;
        private RaycastHit _hit;
        private float _distance;

        public void Initialize(IInputHandler inputHandler, CurrentPlayerInputState currentPlayerInputState,
            EditingButtons editingButtons)
        {
            _currentPlayerInputState = currentPlayerInputState;
            _inputHandler = inputHandler;
            _editingButtons = editingButtons;

            _inputHandler.LeftMouseButtonClicked += OnClicked;
        }

        private void OnClicked()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_currentPlayerInputState.InputState == PlayerInputStates.GridBuildingMainWindow &&
                Physics.Raycast(_ray, out _hit))
            {
                if (_hit.collider.TryGetComponent<Building>(out _building))
                {
                    _positionOnMainCamera = Camera.main.WorldToScreenPoint(_building.CenterTransform.position);

                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        _canvasRectTransform,
                        _positionOnMainCamera,
                        _canvasCamera,
                        out _canvasBuildingPosition
                    );

                    _editingButtons.ShowButtons(_canvasBuildingPosition);
                }
            }
        }

        public void Dispose()
        {
            _inputHandler.LeftMouseButtonClicked -= OnClicked;
        }
    }
}