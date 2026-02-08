using System;
using System.Collections.Generic;
using System.IO;
using Grid;
using Grid.Cells;
using UnityEngine;
using Zenject;

namespace Data
{
    public class DataLoader : MonoBehaviour
    {
        private const string BaseFolderPath = "Data";
        private const string CellStoragePath = "CellStorage";

        public event Action<List<List<Cell>>> CellsLoaded;

        private List<List<Cell>> _cells;

        private BaseStorage _baseStorage;
        private GridModel _gridModel;

        [Inject]
        public void OnConstruct(GridModel gridModel, BaseStorage baseStorage)
        {
            Debug.Log($"Constructing data");
            _gridModel = gridModel;
            _baseStorage = baseStorage;
        }

        public void SaveData()
        {
            Debug.Log($"Saving data {_gridModel.Cells.Count}");
            _baseStorage.Save<IReadOnlyCollection<IReadOnlyCollection<Cell>>>(
                Path.Combine(BaseFolderPath, CellStoragePath), _gridModel.Cells);
        }

        public void LoadData()
        {
            _cells = _baseStorage.Load<List<List<Cell>>>(Path.Combine(BaseFolderPath, CellStoragePath));

            CellsLoaded?.Invoke(_cells);
        }
    }
}