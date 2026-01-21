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
            if (_currentBuilding != null)
                Destroy(_currentBuilding);

            _currentBuilding = building;
            _gridView.Show();
        }

        public void OnTicked()
        {
            if (_currentBuilding == null)
                return;

            _currentBuildingPositionOnGrid = new Vector2Int(Mathf.FloorToInt(_currentBuilding.transform.position.x - Mathf.Abs(_startPosition.x)),
                Mathf.FloorToInt(_currentBuilding.transform.position.z - Mathf.Abs(_startPosition.z)));

            _currentBuilding.ChangeAvailability(_gridModel.IsGridFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y));
            _currentBuilding.ChangeAvailability(false);
        }

        public void OnBuildingPlaced()
        {
            if (_currentBuilding == null)
                return;

            _currentBuildingPositionOnGrid = new Vector2Int(Mathf.FloorToInt(_currentBuilding.transform.position.x - Mathf.Abs(_startPosition.x)),
                Mathf.FloorToInt(_currentBuilding.transform.position.z - Mathf.Abs(_startPosition.z)));

            if(_gridModel.IsGridFree(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y))
            {
                _currentBuilding.Place();
                _gridModel.PlaceBuilding(_currentBuildingPositionOnGrid.x, _currentBuildingPositionOnGrid.y, _currentBuilding);
                _currentBuilding = null;
            }
            else
            {
                Destroy(_currentBuilding);
            }

            _gridView.Hide();
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