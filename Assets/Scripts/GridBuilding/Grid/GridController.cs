using Builders;
using Buildings;
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
        private BuildHandler _buildHandler;
        
        private Building _currentBuilding;
        private Vector2Int _currentBuildingPositionOnGrid;

        private void Start()
        {
            _gridModel.SetSize(_gridSize.x, _gridSize.y);

            _buildHandler.BuildingCreated += OnBuildingCreated;
            _buildHandler.Ticked += OnTicked;
            _buildHandler.BuildingPlaced += OnBuildingPlaced;

            _gridView.Init(_gridSize, _startPosition, 1);
        }

        [Inject]
        public void OnConstruct(GridView gridView, GridModel gridModel, BuildHandler buildHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _buildHandler = buildHandler;
        }

        public void OnBuildingCreated(Building building)
        {
            _currentBuilding = building;
            _gridView.Show();
        }

        public void OnTicked()
        {
            _currentBuildingPositionOnGrid = GetCurrentBuilldingPositionOnGrid();

            _currentBuilding.ChangeAvailability(_gridModel.IsGridFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y));
            Debug.Log(_currentBuildingPositionOnGrid);
        }

        public void OnBuildingPlaced()
        {
            _currentBuildingPositionOnGrid = GetCurrentBuilldingPositionOnGrid();

            if(_gridModel.IsGridFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y))
            {
                _currentBuilding.Place();
                _gridModel.PlaceBuilding(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y, _currentBuilding);
            }
            else
            {
                Destroy(_currentBuilding);
            }

            _gridView.Hide();
        }

        private Vector2Int GetCurrentBuilldingPositionOnGrid()
        {
            return new Vector2Int(Mathf.RoundToInt(_currentBuilding.transform.position.x - _currentBuilding.HalfSize.x - _startPosition.x),
                Mathf.RoundToInt(_currentBuilding.transform.position.z - _currentBuilding.HalfSize.z - _startPosition.z));
        }

        private void OnEnable()
        {
            if (_buildHandler == null)
                return;

            _buildHandler.BuildingCreated += OnBuildingCreated;
            _buildHandler.Ticked += OnTicked;
            _buildHandler.BuildingPlaced += OnBuildingPlaced;
        }

        private void OnDisable()
        {
            if (_buildHandler == null)
                return;

            _buildHandler.BuildingCreated -= OnBuildingCreated;
            _buildHandler.Ticked -= OnTicked;
            _buildHandler.BuildingPlaced -= OnBuildingPlaced;
        }
    }
}