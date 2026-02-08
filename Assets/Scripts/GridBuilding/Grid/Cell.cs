using Buildings;
using System;
using UnityEngine;

namespace Grid.Cells
{
    [Serializable]
    public class Cell
    {
        [field: SerializeField] public BuildingAvailableTypes AvailableBuildingType { get; private set; }
        [field: SerializeField] public int XPosition { get; private set; }
        [field: SerializeField] public int YPosition { get; private set; }
        
        private Building _currentBuilding;

        public void SetCellPosition(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }
        public void SetCurrentBuilding(Building building)
        {
            _currentBuilding = building;
        }

        public void SetAvailableBuildingType(BuildingAvailableTypes buildingType)
        {
            AvailableBuildingType = buildingType;
        }
    }
}

