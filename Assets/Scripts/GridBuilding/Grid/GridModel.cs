using Buildings;
using Grid.Cells;
using System.Collections.Generic;

namespace Grid
{
    public class GridModel
    {
        private List<List<Cell>> _cells;

        public IReadOnlyCollection<IReadOnlyCollection<Cell>> Cells;

        public void SetSize(int sizeX, int sizeY)
        {
            for (int i = 0; i < sizeX; i++) 
            {
                _cells.Add(new List<Cell>());
                for (int j = 0; j < sizeY; j++)
                {
                    _cells[i].Add(null);
                }
            }
        }

        public bool IsGridFree(int x, int y)
        {
            if ((x < _cells.Count && y < _cells[x].Count) == false)
                return false;

            if (_cells[x][y] == null)
            {
                return true;
            }

            return false;
        }

        public void PlaceBuilding(int x, int y, Building building)
        {
               
        }
    }
}
