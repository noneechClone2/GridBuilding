using System;
using System.Collections.Generic;
using System.IO;
using Grid;
using Grid.Cells;

namespace Data.Loaders
{
    public class GridLoader : BaseDataLoader
    {
        public event Action<List<List<Cell>>> DataLoaded;
        
        private const string CellStorageFilePath = "CellStorage";

        private List<List<Cell>> _cells;
        
        private GridModel _gridModel;
        
        public GridLoader(GridModel gridModel, BaseStorage baseStorage) :  base(baseStorage)
        {
            _gridModel = gridModel;
        }


        public override void SaveData()
        {
            if (_baseStorage == null)
            {
                UnityEngine.Debug.Log(1);
            }
            
            _baseStorage.Save<IReadOnlyCollection<IReadOnlyCollection<Cell>>>(
                Path.Combine(BaseFolderPath, CellStorageFilePath), _gridModel.Cells);
        }

        public override void LoadData()
        {
            _cells = _baseStorage.Load<List<List<Cell>>>(Path.Combine(BaseFolderPath, CellStorageFilePath));

            DataLoaded?.Invoke(_cells);
        }
    }
}