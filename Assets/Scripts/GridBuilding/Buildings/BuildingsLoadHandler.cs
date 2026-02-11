using System;
using System.Collections.Generic;
using Builders;
using Grid;
using UnityEditor.PackageManager;
using UnityEngine;
using Zenject;


namespace Buildings
{
    public class BuildingsLoadHandler : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject[] _buildingPrefabs;

        private Dictionary<int, Building> _buildingsIdToBuilding;

        private GridController _gridController;
        private Building _currentBuilding;
        private BuildingData _buildingData;

        [Inject]
        public void OnConstruct(GridController gridController, BuildingData buildingData)
        {
            _gridController = gridController;
            _buildingData = buildingData;
        }

        public void CreateBuilding(int x, int y, int id)
        {
            if (!_buildingsIdToBuilding.ContainsKey(id))
                throw new Exception("Buildings ID " + id + " is not inside building list");

            if (_gridController.GridSize.x < x || _gridController.GridSize.y < y)
                throw new ArgumentOutOfRangeException("Building position " + id + " is outside grid size");

            _currentBuilding = UnityEngine.Object.Instantiate(_buildingsIdToBuilding[id]);
            _currentBuilding.SetPosition(new Vector3(x + _gridController.StartPosition.x, 0, y + _gridController.StartPosition.z));
            _currentBuilding.Init(_buildingData);
            _currentBuilding.Place();
        }

        private void CreateBuildingsDictionary()
        {
            _buildingsIdToBuilding = new Dictionary<int, Building>();

            foreach (var building in _buildingPrefabs)
            {
                _currentBuilding = building.GetComponent<Building>();

                if (_buildingsIdToBuilding.ContainsKey(_currentBuilding.Id))
                    throw new Exception("Buildings ID " + _currentBuilding.Id + " is already in use");

                _buildingsIdToBuilding.Add(_currentBuilding.Id, _currentBuilding);
            }
        }

        public void Initialize()
        {
            CreateBuildingsDictionary();
        }
    }
}