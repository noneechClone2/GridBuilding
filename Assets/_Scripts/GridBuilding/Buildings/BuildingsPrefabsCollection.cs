using System.Collections.Generic;
using UnityEngine;

namespace GridBuilding.Buildings
{
    public class BuildingsPrefabsCollection : MonoBehaviour
    {
        [SerializeField] private Building[]  _buildingsPrefabs;
        
        public IReadOnlyCollection<Building> BuildingsPrefabs => _buildingsPrefabs;
    }
}