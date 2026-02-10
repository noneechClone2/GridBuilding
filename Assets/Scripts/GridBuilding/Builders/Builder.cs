using Grid;
using UnityEngine;
using Buildings;
using Zenject;
using System;

namespace Builders
{
    public class Builder : IInitializable, IDisposable
    {
        private GridView _gridView;
        private BuildHandler _buildHandler;
        private Building _currentBuilding;

        private Plane _rayCastPlane;
        private Vector3Int _buildingPosition;
        private Ray _ray;
        private float _distanceOnRay;
        private bool _isDragging;
        private Vector3 _mouseStartDraggingPosition;
        private Vector3 _buildingStartDraggingPosition;
        private Vector3 _currentMousePosition;

        public Builder(GridView gridView, BuildHandler buildHandler)
        {
            _gridView = gridView;
            _buildHandler = buildHandler;
        }

        private void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
        }

        private void OnTicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }
        }

        private void OnFixedTicked()
        {
            if (_isDragging == false && _mouseStartDraggingPosition != Vector3.zero)
            {
                _mouseStartDraggingPosition = Vector3.zero;
            }
            
            if (_isDragging && _mouseStartDraggingPosition == Vector3.zero)
            {
                _mouseStartDraggingPosition = _currentMousePosition;
                _buildingStartDraggingPosition = _currentBuilding.transform.position - _currentBuilding.HalfSize;
            }
            
            _currentMousePosition = GetMousePosition();
            
            if (_isDragging)
            {
                _buildingPosition = Vector3Int.FloorToInt(_buildingStartDraggingPosition + (_currentMousePosition - _mouseStartDraggingPosition));
                _buildingPosition.x = (int)Mathf.Clamp(_buildingPosition.x, _gridView.StartPosition.x,
                    _gridView.EndPosition.x);
                _buildingPosition.y = 0;
                _buildingPosition.z = (int)Mathf.Clamp(_buildingPosition.z, _gridView.StartPosition.z,
                    _gridView.EndPosition.z);

                _currentBuilding.SetPosition(_buildingPosition);
            }
        }

        private Vector3 GetMousePosition()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_rayCastPlane.Raycast(_ray, out _distanceOnRay))
                return _ray.GetPoint(_distanceOnRay);

            return Vector3.zero;
        }

        public void Initialize()
        {
            _rayCastPlane = new Plane(Vector3.up, _gridView.StartPosition);

            _buildHandler.BuildingCreated += OnBuildingCreated;
            _buildHandler.Ticked += OnTicked;
            _buildHandler.FixedTicked += OnFixedTicked;
        }

        public void Dispose()
        {
            _buildHandler.BuildingCreated -= OnBuildingCreated;
            _buildHandler.Ticked -= OnTicked;
            _buildHandler.FixedTicked -= OnFixedTicked;
        }
    }
}