using UnityEngine;
using Zenject;
using System;
using GridBuilding.Buildings;
using GridBuilding.Grid.View;
using InputHandlers;

namespace GridBuilding.Builders
{
    public class BuildingMover : IDisposable
    {
        private GridView _gridView;
        private BuildHandler _buildHandler;
        private Building _currentBuilding;
        private IInputHandler _inputHandler;
        
        private Vector3Int _buildingStartPosition;
        
        public BuildingMover(GridView gridView, BuildHandler buildHandler, IInputHandler inputHandler)
        {
            _gridView = gridView;
            _buildHandler = buildHandler;
            _inputHandler = inputHandler;
        }

        public void Initialize()
        {
            _buildHandler.BuildingCreated += OnBuildingCreated;
            
            _inputHandler.BuildingMoved += OnBuildingMoved;
            _inputHandler.BuildingMovingStarted += OnBuildingMovingStarted;
        }

        private void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
        }

        private void OnBuildingMovingStarted()
        {
            _buildingStartPosition = Vector3Int.FloorToInt(_currentBuilding.transform.position);
        }

        private void OnBuildingMoved(Vector3Int buildingPosition)
        {
            buildingPosition += _buildingStartPosition;
            buildingPosition.x = (int)Mathf.Clamp(buildingPosition.x, _gridView.StartPosition.x,
                _gridView.EndPosition.x);
            buildingPosition.y = 0;
            buildingPosition.z = (int)Mathf.Clamp(buildingPosition.z, _gridView.StartPosition.z,
                _gridView.EndPosition.z);

            _currentBuilding.SetPosition(buildingPosition);
        }

        public void Dispose()
        {
            _buildHandler.BuildingCreated -= OnBuildingCreated;
            
            _inputHandler.BuildingMoved -= OnBuildingMoved;
            _inputHandler.BuildingMovingStarted -= OnBuildingMovingStarted;
        }
    }
}