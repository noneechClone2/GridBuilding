using System;
using UnityEngine;

namespace Data.Loaders
{
    public abstract class BaseDataLoader
    {
        protected const string BaseFolderPath = "Data";

        protected BaseStorage _baseStorage;

        public BaseDataLoader(BaseStorage baseStorage)
        {
            _baseStorage = baseStorage;
        }

        public abstract void SaveData();

        public abstract void LoadData();
    }
}