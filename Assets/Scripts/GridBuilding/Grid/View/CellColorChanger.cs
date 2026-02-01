using Buildings;
using System;
using UnityEngine;
using Zenject;

namespace Grid.Cells
{
    public class CellAvailabilityChanger : IInitializable, IDisposable
    {
        private readonly Color UnavailableColor = new Color(1f, 0.4f, 0.4f);
        private readonly Color AvailableColor = Color.green;
        private readonly string CellColorId = "_TileColor";

        private GridCollection _gridCollection;
        private GridModel _gridModel;
        private Renderer _currentCellRenderer;

        public CellAvailabilityChanger(GridCollection gridCollection, GridModel gridModel)
        {
            _gridCollection = gridCollection;
            _gridModel = gridModel;
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
            _gridModel.CellAvailabilityChanged += OnCellAvailabilityChanged;
        }
    }
}