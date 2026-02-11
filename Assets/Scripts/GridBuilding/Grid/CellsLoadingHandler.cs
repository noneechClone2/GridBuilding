using System;
using System.Collections.Generic;
using Buildings;
using Data;
using Zenject;

namespace Grid.Cells
{
    public class CellsLoadingHandler : IInitializable, IDisposable
    {
        private GridView _gridView;
        private GridModel _gridModel;
        private CellColorChanger _cellColorChanger;
        private BuildingsLoadHandler _buildingLoadHandler;
        private DataLoader _dataLoader;

        [Inject]
        public void OnConstruct(GridView gridView, GridModel gridModel, CellColorChanger cellColorChanger,
            DataLoader dataLoader, BuildingsLoadHandler buildingLoadHandler)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _dataLoader = dataLoader;
            _cellColorChanger = cellColorChanger;
            _buildingLoadHandler = buildingLoadHandler;
        }

        private void OnCellsLoaded(List<List<Cell>> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    if (cells[i][j].CurrentBuildingId >= 0)
                    {
                        _buildingLoadHandler.CreateBuilding(i, j, cells[i][j].CurrentBuildingId);
                    }
                }
            }

            _gridModel.SetCellsCollection(cells);
            _gridView.SetCellCollection(cells);
            _cellColorChanger.SetCellCollection(cells);
        }

        public void Initialize()
        {
            _dataLoader.CellsLoaded += OnCellsLoaded;
        }

        public void Dispose()
        {
            _dataLoader.CellsLoaded -= OnCellsLoaded;
        }
    }
}