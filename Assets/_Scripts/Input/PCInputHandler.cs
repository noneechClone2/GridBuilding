using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InputHandlers
{
    public class PCInputHandler : ITickable, IFixedTickable, IInputHandler
    {
        public event Action LeftMouseButtonStartDragging;
        public event Action LeftMouseButtonStoppedDragging;
        public event Action<Vector3Int> BuildingMoved;

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

        public async void Tick()
        {
            if (Input.GetMouseButtonDown(0) && !_isMouseAcitonPerforming)
            {
                await MouseAction();
            }
        }

        public void FixedTick()
        {
            if (_isMouseDragging)
            {
                _currentMousePosition = GetMousePosition();
            }
        }

        private async UniTask MouseAction()
        {
            _mouseStartMovingPosition = GetMousePosition();
            while (Input.GetMouseButton(0))
            {
                _dragTime += Time.deltaTime;
                Debug.Log(_dragTime);

                if (_dragTime >= _minDragTime)
                {
                    if (!_isMouseDragging)
                    {
                        LeftMouseButtonStartDragging?.Invoke();
                        _isMouseDragging = true;
                    }
                    _currentMousePosition = GetMousePosition();
                    BuildingMoved?.Invoke(Vector3Int.FloorToInt((_currentMousePosition - _mouseStartMovingPosition) / _buildingMoveDevider));
                }
                
                await UniTask.Yield();
            }

            if (_isMouseDragging)
            {
                LeftMouseButtonStoppedDragging?.Invoke();
                _isMouseDragging = false;
            }
            
            _dragTime = 0;
        }
        
        private Vector3 GetMousePosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_rayCastPlane.Raycast(_ray, out float distanceOnRay))
                return _ray.GetPoint(distanceOnRay);

            return Vector3.zero;
        }
    }
}