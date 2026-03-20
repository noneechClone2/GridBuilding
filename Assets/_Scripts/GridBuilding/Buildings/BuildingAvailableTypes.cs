using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Buildings
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

