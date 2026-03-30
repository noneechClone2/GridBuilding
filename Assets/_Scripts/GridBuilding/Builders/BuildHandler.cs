using System;
using UnityEngine;
using GridBuilding.Buildings;
using Zenject;

namespace GridBuilding.Builders
{
    public class BuildHandler : MonoBehaviour
    {
        public event Action<Building> BuildingCreated;
        public event Action Ticked;
        public event Action BuildingPlaced;

        [SerializeField] private Transform _buildingsParentTransform;
        
        private Building _currentBuilding;
        private BuildingData _buildingData;

        private void Update()
        {
            if (_currentBuilding != null)
            {
                Ticked?.Invoke();
            }
        }

        public void Initialize(BuildingData buildingData)
        {
            _buildingData = buildingData;
        }

        public void CreateBuilding(Building buildingPrefab)
        {
            if (buildingPrefab == null)
                throw new Exception("Empty building prefab");

            _currentBuilding = Instantiate(buildingPrefab, _buildingsParentTransform);
            _currentBuilding.Init(_buildingData);
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
