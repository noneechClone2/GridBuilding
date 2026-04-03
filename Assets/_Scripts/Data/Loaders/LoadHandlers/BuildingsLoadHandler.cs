using System;
using System.Collections.Generic;
using Data.Loaders;
using GridBuilding.Buildings;
using GridBuilding.Grid;
using UnityEngine;
using Zenject;
using Object = System.Object;


namespace Data
{
    public class BuildingsLoadHandler : IDisposable
    {
        private BuildingsPrefabsCollection _buildingsPrefabs;
        private Dictionary<int, Building> _buildingsIdToBuilding;

        private Building _currentBuilding;
        private BuildingsLoader _buildingLoader;
        private BuildingMaterials _buildingMaterials;

        public BuildingsLoadHandler(BuildingMaterials buildingMaterials, BuildingsLoader buildingsLoader,
            BuildingsPrefabsCollection buildingsPrefabs)
        {
            _buildingLoader = buildingsLoader;
            _buildingMaterials = buildingMaterials;
            _buildingsPrefabs = buildingsPrefabs;
        }

        private void OnDataLoaded(List<BuildingData> buildingDatas)
        {
            foreach (var buildingData in buildingDatas)
            {
                var currentBuinding = UnityEngine.Object.Instantiate(_buildingsIdToBuilding[buildingData.Id]);
                
                currentBuinding.Init(_buildingMaterials);
                currentBuinding.SetPosition(new Vector3Int(buildingData.XPosition, 0, buildingData.YPosition));
                currentBuinding.Rotate(buildingData.Rotation);
                currentBuinding.Place();
            }
        }

        private void CreateBuildingsDictionary()
        {
            _buildingsIdToBuilding = new Dictionary<int, Building>();

            foreach (var buildingPrefab in _buildingsPrefabs.BuildingsPrefabs)
            {
                if (_buildingsIdToBuilding.ContainsKey(buildingPrefab.Id))
                    throw new Exception("Buildings ID " + buildingPrefab.Id + " is already in use");

                _buildingsIdToBuilding.Add(buildingPrefab.Id, buildingPrefab);
            }
        }

        public void Initialize()
        {
            CreateBuildingsDictionary();
            _buildingLoader.DataLoaded += OnDataLoaded;
        }
        
        public void Dispose()
        {
            _buildingLoader.DataLoaded -= OnDataLoaded;
        }
    }
}