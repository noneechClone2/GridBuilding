using System;
using GridBuilding.Builders;
using GridBuilding.Buildings;
using GridBuilding.Grid.View;
using UnityEngine;

namespace GridBuilding.Grid
{
    public class GridController : IDisposable
    {
        private GridView _gridView;
        private GridModel _gridModel;
        private BuildHandler _buildHandler;

        private Building _currentBuilding;
        private Vector2Int _currentBuildingPositionOnGrid;
        
        [field: SerializeField] public Vector2Int GridSize {get; private set;} = new Vector2Int(10, 10);
        [field: SerializeField] public Vector3 StartPosition {get; private set;} = new Vector3(-5, 0.1f, -5);
        
        public GridController(GridView gridView, GridModel gridModel, BuildHandler buildHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _buildHandler = buildHandler;
        }

        private void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
            _gridView.Show();
        }

        private void OnTicked()
        {
            _currentBuildingPositionOnGrid = GetCurrentBuilldingPositionOnGrid();

            _currentBuilding.ChangeAvailability(_gridModel.IsGridsFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y, _currentBuilding));
        }

        private void OnBuildingPlaced()
        {
            _currentBuildingPositionOnGrid = GetCurrentBuilldingPositionOnGrid();

            if (_gridModel.IsGridsFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y, _currentBuilding))
                _gridModel.PlaceBuilding(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y, _currentBuilding);
            else
                UnityEngine.Object.Destroy(_currentBuilding.gameObject);
            
            _gridView.Hide();
        }

        private Vector2Int GetCurrentBuilldingPositionOnGrid()
        {
            return new Vector2Int(Mathf.FloorToInt(_currentBuilding.transform.position.x - _currentBuilding.HalfSize.x - StartPosition.x),
                Mathf.FloorToInt(_currentBuilding.transform.position.z - _currentBuilding.HalfSize.z - StartPosition.z));
        }

        public void Initialize()
        {
            _buildHandler.BuildingCreated += OnBuildingCreated;
            _buildHandler.Ticked += OnTicked;
            _buildHandler.BuildingPlaced += OnBuildingPlaced;
            
            _gridModel.SetSize(GridSize.x, GridSize.y);
            _gridView.Init(GridSize, StartPosition, 0.3f);
        }

        public void Dispose()
        {
            _buildHandler.BuildingCreated -= OnBuildingCreated;
            _buildHandler.Ticked -= OnTicked;
            _buildHandler.BuildingPlaced -= OnBuildingPlaced;
        }
    }
}
