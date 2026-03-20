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
        [field: SerializeField] public int CurrentBuildingId = -1;

        public void SetCellPosition(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }

        public void SetCurrentBuilding(int buildingId)
        {
            CurrentBuildingId = buildingId;
        }

        public void SetAvailableBuildingType(BuildingAvailableTypes buildingType)
        {
            AvailableBuildingType = buildingType;
        }
    }
}