using Buildings;
using System;
using UnityEngine;

namespace Grid.Cells
{
    [Serializable]
    public class Cell
    {
        [SerializeField] private Vector2Int _positionOnGrid;
        [SerializeField] private BuildingAvailableTypes _availableBuildingType;
        [SerializeField, HideInInspector] private Building _currentBuilding;

        public Building CurrentBuilding => _currentBuilding;
        public Vector2Int PositionOnGrid => _positionOnGrid;
        public BuildingAvailableTypes AvailableBuildingType => _availableBuildingType;

        public void SetCurrentBuilding(Building building)
        {
            _currentBuilding = building;
        }

        public void SetAvailableBuildingType(BuildingAvailableTypes buildingType)
        {
            _availableBuildingType = buildingType;
        }
    }
}

