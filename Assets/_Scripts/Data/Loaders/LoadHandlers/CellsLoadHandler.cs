using System;
using System.Collections.Generic;
using Data;
using Data.Loaders;
using GridBuilding.Grid.View;

namespace GridBuilding.Grid.Cells
{
    public class CellsLoadHandler : IDisposable
    {
        private GridView _gridView;
        private GridModel _gridModel;
        private CellColorChanger _cellColorChanger;
        private GridLoader _dataLoader;
        
        public CellsLoadHandler(GridView gridView, GridModel gridModel, CellColorChanger cellColorChanger,
            GridLoader dataLoader)
        {
            _gridView = gridView;
            _gridModel = gridModel;
            _dataLoader = dataLoader;
            _cellColorChanger = cellColorChanger;
        }

        public void Initialize()
        {
            _dataLoader.DataLoaded += OnCellsLoaded;
        }

        private void OnCellsLoaded(List<List<Cell>> cells)
        {
            _gridModel.SetCellsCollection(cells);
            _gridView.SetCellCollection(cells);
            _cellColorChanger.SetCellCollection(cells);
        }

        public void Dispose()
        {
            _dataLoader.DataLoaded -= OnCellsLoaded;
        }
    }
}