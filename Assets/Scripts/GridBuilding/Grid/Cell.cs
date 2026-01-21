using Buildings;
using System;
using UnityEngine;

namespace Grid.Cells
{
    [Serializable]
    public class Cell
    {
        [SerializeField] private Vector2 _positionOnGrid;
        [SerializeField] private BuildingTypes[] _availableBuildingTypes;
        private Building _currentBuilding;

        public Building CurrentBuilding => _currentBuilding;
        public Vector2 PositionOnGrid => _positionOnGrid;
        public BuildingTypes[] AvailableBuildingTypes => _availableBuildingTypes;

        public void SetCurrentBuilding(Building building)
        {
            _currentBuilding = building;
        }
    }
}

