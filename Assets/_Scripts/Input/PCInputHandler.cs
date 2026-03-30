using System;
using Cysharp.Threading.Tasks;
using InputHandlers.PlayerInputState;
using UnityEngine;
using Zenject;

namespace InputHandlers
{
    public class PCInputHandler : ITickable, IFixedTickable, IInputHandler
    {
        public event Action BuildingMovingStarted;
        public event Action BuildingMovingStopped;
        public event Action<Vector3Int> BuildingMoved;
        public event Action LeftMouseButtonClicked;

        private CurrentPlayerInputState _playerInputState;
        private Plane _rayCastPlane = new Plane(Vector3.up, Vector3.zero);
        private Ray _ray;

        private Vector3 _buildingStartMovingPosition;
        private Vector3 _currentMousePosition;
        private Vector3 _mouseStartMovingPosition;


        private bool _isMouseAcitonPerforming;
        private bool _isMouseDragging;
        private int _buildingMoveDevider = 2;
        private float _dragTime;
        private float _minDragTime = 0.2f;

        private PCInputHandler(CurrentPlayerInputState playerInputState)
        {
            _playerInputState = playerInputState;
        }

        public async void Tick()
        {
            if (Input.GetMouseButtonDown(0) && !_isMouseAcitonPerforming)
            {
                _isMouseAcitonPerforming = true;
                await MouseAction();
            }
        }

        public void FixedTick()
        {
            if (_isMouseDragging)
            {
                _currentMousePosition = GetMousePositionOnPlane();
            }
        }

        private async UniTask MouseAction()
        {
            _mouseStartMovingPosition = GetMousePositionOnPlane();
            while (Input.GetMouseButton(0))
            {
                _dragTime += Time.deltaTime;

                if (_dragTime >= _minDragTime)
                {
                    if (!_isMouseDragging)
                    {
                        if(_playerInputState.InputState == PlayerInputStates.BuildingEditor)
                            BuildingMovingStarted?.Invoke();
                        
                        _isMouseDragging = true;
                    }

                    _currentMousePosition = GetMousePositionOnPlane();

                    if (_playerInputState.InputState == PlayerInputStates.BuildingEditor)
                        BuildingMoved?.Invoke(Vector3Int.FloorToInt(
                            (_currentMousePosition - _mouseStartMovingPosition) / _buildingMoveDevider));
                }

                await UniTask.Yield();
            }

            if (_isMouseDragging)
            {
                BuildingMovingStopped?.Invoke();
                _isMouseDragging = false;
            }
            else
            {
                LeftMouseButtonClicked?.Invoke();
            }

            _isMouseAcitonPerforming = false;
            _dragTime = 0;
        }

        private Vector3 GetMousePositionOnPlane()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_rayCastPlane.Raycast(_ray, out float distanceOnRay))
                return _ray.GetPoint(distanceOnRay);

            return Vector3.zero;
        }
    }
}