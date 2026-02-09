using Buildings;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Grid.Cells
{
    [Serializable]
    public class Cell
    {
        [JsonConverter(typeof(StringEnumConverter))] [field: SerializeField]
        public BuildingAvailableTypes AvailableBuildingType;
        [field: SerializeField] public int XPosition;
        [field: SerializeField] public int YPosition;
        
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

