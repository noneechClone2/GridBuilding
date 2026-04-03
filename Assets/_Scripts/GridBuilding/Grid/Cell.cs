using System;
using GridBuilding.Buildings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace GridBuilding.Grid.Cells
{
    [Serializable]
    public class Cell
    {
        [JsonConverter(typeof(StringEnumConverter))] [field: SerializeField]
        public BuildingAvailableTypes AvailableBuildingType;

        [field: SerializeField] public int XPosition;
        [field: SerializeField] public int YPosition;

        public void SetCellPosition(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }

        public void SetAvailableBuildingType(BuildingAvailableTypes buildingType)
        {
            AvailableBuildingType = buildingType;
        }
    }
}