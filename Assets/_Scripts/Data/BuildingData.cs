using GridBuilding.Buildings;
using UnityEngine;

namespace Data
{
    public struct BuildingData
    {
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public Rotation Rotation { get; set; }
        [field: SerializeField] public int XPosition { get; set; }
        [field: SerializeField] public int YPosition { get; set; }
    }
}