using System;
using System.Collections.Generic;
using System.IO;
using Grid;
using Grid.Cells;
using UnityEngine;

namespace Data.Loaders
{
    public abstract class BaseDataLoader<T> : MonoBehaviour
    {
        protected const string BaseFolderPath = "Data";

        public abstract event Action<T> DataLoaded;

        protected BaseStorage _baseStorage;

        public BaseDataLoader(BaseStorage baseStorage)
        {
            _baseStorage = baseStorage;
        }

        public abstract void SaveData();

        public abstract void LoadData();
    }
}