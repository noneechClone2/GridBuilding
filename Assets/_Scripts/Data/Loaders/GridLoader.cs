using System;
using System.Collections.Generic;
using GridBuilding.Grid;
using GridBuilding.Grid.Cells;

namespace Data.Loaders
{
    public class GridLoader : BaseDataLoader
    {
        public event Action<List<List<Cell>>> DataLoaded;
        
        private const string CellStorageFilePath = "CellStorage";

        private List<List<Cell>> _cells;
        
        private GridModel _gridModel;
        
        public GridLoader(GridModel gridModel, IStorage storage) :  base(storage)
        {
            _gridModel = gridModel;
        }


        public override void SaveData()
        {
            _storage.Save<IReadOnlyCollection<IReadOnlyCollection<Cell>>>(CellStorageFilePath, _gridModel.Cells);
        }

        public override void LoadData()
        {
            _cells = _storage.Load<List<List<Cell>>>(CellStorageFilePath);

            DataLoaded?.Invoke(_cells);
        }
    }
}