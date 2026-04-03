using System;
using System.Collections.Generic;
using UnityEngine;
using GridBuilding.Buildings;

namespace GridBuilding.Builders
{
    public class BuildHandler : MonoBehaviour
    {
        public event Action<Building> BuildingCreated;
        public event Action Ticked;
        public event Action<Building> BuildingPlaced;

        [SerializeField] private Transform _buildingsParentTransform;

        // private int _buildingId;
        // private Stack<int> _deletedBuildingIds;
        private Building _currentBuilding;
        private BuildingMaterials _buildingMaterials;

        private void Update()
        {
            if (_currentBuilding != null)
            {
                Ticked?.Invoke();
            }
        }

        public void Initialize(BuildingMaterials buildingMaterials)
        {
            _buildingMaterials = buildingMaterials;
        }

        public void CreateBuilding(Building buildingPrefab)
        {
            if (buildingPrefab == null)
                throw new Exception("Empty building prefab");

            _currentBuilding = Instantiate(buildingPrefab, _buildingsParentTransform);

            // if (_deletedBuildingIds.Count == 0)
            // {
            //     _currentBuilding.Init(_buildingMaterials, _buildingId);
            //     _buildingId++;
            // }
            // else
            // {
            //     _currentBuilding.Init(_buildingMaterials, _deletedBuildingIds.Pop());
            // }
            
            _currentBuilding.Init(_buildingMaterials);
            
            _currentBuilding.OnCreated();

            BuildingCreated?.Invoke(_currentBuilding);
        }

        public void PlaceBuilding()
        {
            if (_currentBuilding == null)
                throw new Exception("no building");
            
            _currentBuilding.Place();
            BuildingPlaced?.Invoke(_currentBuilding);
            _currentBuilding = null;
        }
    }

}
