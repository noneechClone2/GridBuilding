using System.Collections.Generic;
using Data.Loaders;
using UnityEngine;
using Zenject;

namespace Data
{
    public class GameSaveLoadService : MonoBehaviour
    {
        private List<BaseDataLoader> _loaders;
        
        [Inject]
        public void OnConstruct(GridLoader gridLoader, BuildingsLoader buildingLoader)
        {
            _loaders = new List<BaseDataLoader>() { gridLoader, buildingLoader };
        }

        public void Save()
        {
            foreach (var loader in _loaders)
            {
                loader.SaveData();
            }
        }
        
        public void Load()
        {
            foreach (var loader in _loaders)
            {
                loader.LoadData();
            }
        }
    }
}