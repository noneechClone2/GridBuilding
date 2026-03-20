using System.Collections.Generic;
using Data.Loaders;
using UnityEngine;
using Zenject;

namespace _Scripts.Data
{
    public class GameSaveLoadService : MonoBehaviour
    {
        private List<BaseDataLoader> _loaders;
        
        [Inject]
        public void OnConstruct(GridLoader gridLoader)
        {
            _loaders = new List<BaseDataLoader>() { gridLoader };
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