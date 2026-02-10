using Grid;
using UnityEngine;
using Buildings;
using Zenject;
using System;
using InputHandlers;

namespace Builders
{
    public class Builder : IInitializable, IDisposable
    {
        private GridView _gridView;
        private BuildHandler _buildHandler;
        private Building _currentBuilding;
        private IInputHandler _inputHandler;

        private Vector3Int _buildingPosition;
        private Vector3 _buildingStartDraggingPosition;
        private Vector3 _mouseStartDraggingPosition;
        private Vector3 _currentMousePosition;
        private Plane _rayCastPlane;
        private Ray _ray;
        private float _distanceOnRay;
        
        public Builder(GridView gridView, BuildHandler buildHandler, IInputHandler inputHandler)
        {
            _gridView = gridView;
            _buildHandler = buildHandler;
            _inputHandler = inputHandler;
        }

        private void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;

            _mouseStartDraggingPosition = _currentMousePosition;
            _buildingStartDraggingPosition = _currentBuilding.transform.position - _currentBuilding.HalfSize;
        }
        
        private void OnFixedTicked()
        {
            _currentMousePosition = GetMousePosition();
        }
        
        private void OnMouseButtonPressed()
        {
            if (_currentBuilding == null)
                return;
            
            _mouseStartDraggingPosition = _currentMousePosition;
            _buildingStartDraggingPosition = _currentBuilding.transform.position - _currentBuilding.HalfSize;
        }

        private void OnMouseDragged()
        {
            if (_currentBuilding == null)
                return;
            
            SetBuildingPosition();
        }
        
        private void SetBuildingPosition()
        {
        _buildingPosition = Vector3Int.FloorToInt(_buildingStartDraggingPosition + (_currentMousePosition - _mouseStartDraggingPosition));
            _buildingPosition.x = (int)Mathf.Clamp(_buildingPosition.x, _gridView.StartPosition.x,
                _gridView.EndPosition.x);
            _buildingPosition.y = 0;
            _buildingPosition.z = (int)Mathf.Clamp(_buildingPosition.z, _gridView.StartPosition.z,
                _gridView.EndPosition.z);

            _currentBuilding.SetPosition(_buildingPosition);
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
            _buildHandler.FixedTicked += OnFixedTicked;

            _inputHandler.MouseButtonPressed += OnMouseButtonPressed;
            _inputHandler.MouseDragged += OnMouseDragged;
        }

        public void Dispose()
        {
            _buildHandler.BuildingCreated -= OnBuildingCreated;
            _buildHandler.FixedTicked -= OnFixedTicked;
            
            _inputHandler.MouseButtonPressed -= OnMouseButtonPressed;
            _inputHandler.MouseDragged -= OnMouseDragged;
        }
    }
}