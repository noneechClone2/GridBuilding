using System;
using UnityEngine;
using Buildings;
using Grid;

namespace Builders
{
    public class BuildHandler : MonoBehaviour
    {
        public event Action<Building> BuildingCreated;
        public event Action Ticked;
        public event Action FixedTicked;
        public event Action BuildingPlaced;

        [SerializeField] private Transform _buildingsParentTransform;
        [SerializeField] private GridView _gridView;

        private Building _currentBuilding;

        private void Update()
        {
            if (_currentBuilding != null)
            {
                Ticked?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (_currentBuilding != null)
            {
                FixedTicked?.Invoke();
            }
        }

        public void CreateBuilding(Building buildingPrefab)
        {
            if (buildingPrefab == null)
                throw new Exception("Empty building prefab");

            _currentBuilding = Instantiate(buildingPrefab, _buildingsParentTransform);
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
