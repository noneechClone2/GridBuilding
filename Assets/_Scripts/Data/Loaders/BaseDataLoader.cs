using System;
using UnityEngine;

namespace Data.Loaders
{
    public abstract class BaseDataLoader
    {
        protected IStorage _storage;

        public BaseDataLoader(IStorage storage)
        {
            _storage = storage;
        }

        public abstract void SaveData();

        public abstract void LoadData();
    }
}