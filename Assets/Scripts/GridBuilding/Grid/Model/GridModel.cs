using Buildings;
using Grid.Cells;
using System;
using System.Collections.Generic;

namespace Grid
{
    public class GridModel
    {
        public Action<int, int, bool, BuildingAvailableTypes> CellAvailabilityChanged;

        private List<List<Cell>> _cells;

        public IReadOnlyCollection<IReadOnlyCollection<Cell>> Cells =>  _cells;

        private BuildingAvailableTypes _currentCellAvailableType;
        private int _currentCellXPosition;
        private int _currentCellYPosition;

        public void SetSize(int sizeX, int sizeY)
        {
            _cells = new List<List<Cell>>();
            for (int i = 0; i < sizeX; i++)
            {
                if (_cells.Count <= i)
                    _cells.Add(new List<Cell>());

                for (int j = 0; j < sizeY; j++)
                {
                    if (_cells[i].Count <= j)
                    {
                        _cells[i].Add(new Cell());
                        _cells[i][j].SetCellPosition(i, j);
                        _cells[i][j].SetAvailableBuildingType(BuildingAvailableTypes.Everything);
                    }
                }
            }
        }

        public bool IsGridsFree(int x, int y, Building building)
        {
            if (x < 0 || y < 0 || x >= _cells.Count || y >= _cells[0].Count)
                return false;

            foreach (var cell in building.OccupiedCells)
            {
                _currentCellXPosition = x + cell.XPosition;
                _currentCellYPosition = y + cell.YPosition;

                if ((_currentCellXPosition < 0 || _cells.Count <= _currentCellXPosition
                                               || _currentCellYPosition < 0 ||
                                               _cells[0].Count <= _currentCellYPosition))
                {
                    return false;
                }

                _currentCellAvailableType = _cells[_currentCellXPosition][_currentCellYPosition].AvailableBuildingType;

                if (_currentCellAvailableType == BuildingAvailableTypes.Everything)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public void PlaceBuilding(int x, int y, Building building)
        {
            _cells[x][y].SetCurrentBuilding(building);
            foreach (var ocuppiedCell in building.OccupiedCells)
            {
                _currentCellXPosition = x + ocuppiedCell.XPosition;
                _currentCellYPosition = y + ocuppiedCell.YPosition;

                _cells[_currentCellXPosition][_currentCellYPosition] = ocuppiedCell;
                CellAvailabilityChanged?.Invoke(_currentCellXPosition, _currentCellYPosition, false,
                    ocuppiedCell.AvailableBuildingType);
            }
        }
    }
}