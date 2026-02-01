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
        public event Action BuildingPlaced;

        [SerializeField] private Transform _buildingsParentTransform;
        [SerializeField] private GridView _gridView;

        private Building _currentBuilding;

        private void Update()
        {
            if (_currentBuilding != null && Input.GetMouseButtonDown(0))
            {
                BuildingPlaced?.Invoke();
                _currentBuilding = null;
            }
            
            if (_currentBuilding != null)
            {
                Ticked?.Invoke();
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
    }

}
