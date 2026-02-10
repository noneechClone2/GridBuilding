using System;
using System.Collections.Generic;
using Builders;
using Buildings;
using Data;
using Grid.Cells;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector2Int _gridSize;

        private GridView _gridView;
        private GridModel _gridModel;
        private CellColorChanger _cellColorChanger;
        private BuildHandler _buildHandler;
        private DataLoader _dataLoader;

        private Building _currentBuilding;
        private Vector2Int _currentBuildingPositionOnGrid;

        private void Start()
        {
            _gridModel.SetSize(_gridSize.x, _gridSize.y);
            _gridView.Init(_gridSize, _startPosition, 1);
        }

        private void OnEnable()
        {
            _buildHandler.BuildingCreated += OnBuildingCreated;
            _buildHandler.Ticked += OnTicked;
            _buildHandler.BuildingPlaced += OnBuildingPlaced;
            _dataLoader.CellsLoaded += OnCellsLoaded;
        }

        private void OnDisable()
        {
            _buildHandler.BuildingCreated -= OnBuildingCreated;
            _buildHandler.Ticked -= OnTicked;
            _buildHandler.BuildingPlaced -= OnBuildingPlaced;
            
            _dataLoader.CellsLoaded -= OnCellsLoaded;
        }

        [Inject]
        public void OnConstruct(GridView gridView, GridModel gridModel, BuildHandler buildHandler, CellColorChanger cellColorChanger, DataLoader dataLoader)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _buildHandler = buildHandler;
            _dataLoader = dataLoader;
            _cellColorChanger = cellColorChanger;
        }

        private void OnCellsLoaded(List<List<Cell>> cells)
        {
            _gridModel.SetCellsCollection(cells);
            _gridView.CellsLoaded(cells);
            _cellColorChanger.CellCollectionLoaded(cells);
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
                Destroy(_currentBuilding.gameObject);
            
            _gridView.Hide();
        }

        private Vector2Int GetCurrentBuilldingPositionOnGrid()
        {
            return new Vector2Int(Mathf.RoundToInt(_currentBuilding.transform.position.x - _currentBuilding.HalfSize.x - _startPosition.x),
                Mathf.RoundToInt(_currentBuilding.transform.position.z - _currentBuilding.HalfSize.z - _startPosition.z));
        }
    }
}