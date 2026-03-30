using System;
using System.Runtime.Serialization;

namespace GridBuilding.Buildings
{ 
    [Serializable]  
    public enum BuildingAvailableTypes
    {
        [EnumMember(Value = "everything")]
        Everything,
        [EnumMember(Value = "none")]
        None,
        Chair,
        Table,
    }
}

