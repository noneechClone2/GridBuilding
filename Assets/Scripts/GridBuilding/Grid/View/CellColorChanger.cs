using Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grid.Cells
{
    public class CellColorChanger : IInitializable, IDisposable
    {
        private readonly Color UnavailableColor = new Color(1f, 0.4f, 0.4f);
        private readonly Color AvailableColor = Color.green;
        private readonly string CellColorId = "_TileColor";

        private GridCollection _gridCollection;
        private GridModel _gridModel;
        private Renderer _currentCellRenderer;

        public CellColorChanger(GridCollection gridCollection, GridModel gridModel)
        {
            _gridCollection = gridCollection;
            _gridModel = gridModel;
        }

        public void CellCollectionLoaded(List<List<Cell>> cells)
        {
            for (int x = 0; x < cells.Count; x++)
            {
                for (int y = 0; y < cells[x].Count; y++)
                {
                    _currentCellRenderer = _gridCollection.GetObjectRenderer(cells[x][y].XPosition, cells[x][y].YPosition);
                    
                    if(cells[x][y].AvailableBuildingType == BuildingAvailableTypes.None)
                        _currentCellRenderer.material.SetColor(CellColorId, UnavailableColor);
                    else 
                        _currentCellRenderer.material.SetColor(CellColorId, AvailableColor);
                }
            }
        }

        private void OnCellAvailabilityChanged(int x, int y, bool isAvailable, BuildingAvailableTypes availableTypes)
        {
            _currentCellRenderer = _gridCollection.GetObjectRenderer(x, y);

            if (isAvailable)
                _currentCellRenderer.material.SetColor(CellColorId, AvailableColor);
            else 
                _currentCellRenderer.material.SetColor(CellColorId, UnavailableColor);
        }

        public void Initialize()
        {
            _gridModel.CellAvailabilityChanged += OnCellAvailabilityChanged;
        }

        public void Dispose()
        {
            _gridModel.CellAvailabilityChanged -= OnCellAvailabilityChanged;
        }
    }
}