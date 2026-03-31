using System;
using System.Collections.Generic;
using GridBuilding.Buildings;
using GridBuilding.Grid;
using UnityEngine;
using Zenject;


namespace Data
{
    public class BuildingsLoadHandler : IInitializable
    {
        private BuildingsPrefabsCollection _buildingsPrefabs;
        private Dictionary<int, Building> _buildingsIdToBuilding;

        private GridController _gridController;
        private Building _currentBuilding;
        private BuildingMaterials _buildingMaterials;
        
        public BuildingsLoadHandler(GridController gridController, BuildingMaterials buildingMaterials,  BuildingsPrefabsCollection buildingsPrefabs)
        {
            _gridController = gridController;
            _buildingMaterials = buildingMaterials;
            _buildingsPrefabs = buildingsPrefabs;
        }

        public void CreateBuilding(int x, int y, int id)
        {
            if (!_buildingsIdToBuilding.ContainsKey(id))
                throw new Exception("Buildings ID " + id + " is not inside building list");

            if (_gridController.GridSize.x < x || _gridController.GridSize.y < y)
                throw new ArgumentOutOfRangeException("Building position " + id + " is outside grid size");

            _currentBuilding = UnityEngine.Object.Instantiate(_buildingsIdToBuilding[id]);
            _currentBuilding.SetPosition(new Vector3(x + _gridController.StartPosition.x, 0, y + _gridController.StartPosition.z));
            _currentBuilding.Init(_buildingMaterials);
            _currentBuilding.Place();
        }

        private void CreateBuildingsDictionary()
        {
            _buildingsIdToBuilding = new Dictionary<int, Building>();

            foreach (var buildingPrefab in _buildingsPrefabs.BuildingsPrefabs)
            {
                // _currentBuilding = building.GetComponent<Building>();

                if (_buildingsIdToBuilding.ContainsKey(buildingPrefab.Id))
                    throw new Exception("Buildings ID " + buildingPrefab.Id + " is already in use");

                _buildingsIdToBuilding.Add(buildingPrefab.Id, buildingPrefab);
            }
        }

        public void Initialize()
        {
            CreateBuildingsDictionary();
        }
    }
}