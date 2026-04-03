using System;
using System.Collections.Generic;
using GridBuilding.Builders;
using GridBuilding.Buildings;

namespace Data.Loaders
{
    public class BuildingsLoader : BaseDataLoader
    {
        private readonly string StoragePath = "BuildingsStorage";
        
        public event Action<List<BuildingData>> DataLoaded;
        
        private List<BuildingData> _buildings;
        
        private BuildHandler _buildHandler;
        
        public BuildingsLoader(IStorage storage, BuildHandler buildHandler) : base(storage)
        {
            _buildHandler = buildHandler;
            _buildings = new List<BuildingData>();
        }

        public void Initialize()
        {
            _buildHandler.BuildingPlaced += OnBuildingPlaced; 
        }
        
        public override void SaveData()
        {
            _storage.Save<List<BuildingData>>(StoragePath, _buildings);
        }

        public override void LoadData()
        {
            _buildings = _storage.Load<List<BuildingData>>(StoragePath);
            DataLoaded?.Invoke(_buildings);
        }
        
        private void OnBuildingPlaced(Building building)
        {
            var buildingData = new BuildingData();
            buildingData.Id = building.Id;
            buildingData.XPosition = building.XPosition;
            buildingData.YPosition = building.YPosition;
            buildingData.Rotation = building.Rotation;
            _buildings.Add(buildingData);
            
        }
    }
}