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
            _cells = new List<List<Cell>>();
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
            //UnityEngine.Debug.Log(x + " " + y);
            if (x < 0 || y < 0 || x >= _cells.Count || y >= _cells[0].Count)
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
