using System;
using UnityEngine;
using GridBuilding.Buildings;

namespace GridBuilding.Builders
{
    public class BuildHandler : MonoBehaviour
    {
        public event Action<Building> BuildingCreated;
        public event Action Ticked;
        public event Action BuildingPlaced;

        [SerializeField] private Transform _buildingsParentTransform;
        
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
            _currentBuilding.Init(_buildingMaterials);
            _currentBuilding.OnCreated();

            BuildingCreated?.Invoke(_currentBuilding);
        }

        public void PlaceBuilding()
        {
            if (_currentBuilding == null)
                throw new Exception("no building");
            
            BuildingPlaced?.Invoke();
            _currentBuilding.Place();
            _currentBuilding = null;
        }
    }

}
