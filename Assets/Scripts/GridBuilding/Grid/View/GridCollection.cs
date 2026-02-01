using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridCollection
    {
        private List<List<GameObject>> _cells;
        private List<List<Renderer>> _cellRenderers;

        public IReadOnlyCollection<IReadOnlyCollection<Renderer>> CellRenderers => _cellRenderers;
        public IReadOnlyCollection<IReadOnlyCollection<GameObject>> Cells => _cells;
        public int XSize => _cells.Count;
        public int YSize => _cells[0].Count;

        public GameObject GetObject(int x, int y)
        {
            return _cells[x][y];
        }

        public Renderer GetObjectRenderer(int x, int y)
        {
            return _cellRenderers[x][y];
        }

        public void CreateCollection(int sizeX, int sizeY, GameObject tilePrefab, float tileScale, Vector3 startPosition, Transform parentTransform = null)
        {
            if(_cells == null)
            {
                _cells = new();
                _cellRenderers = new();
            }

            for (int i = 0; i < sizeX; i++)
            {
                if(_cells.Count <= i)
                {
                    _cells.Add(new());
                    _cellRenderers.Add(new());
                }
                
                for (int j = 0; j < sizeY; j++)
                {
                    if (_cells[i].Count <= j)
                    {
                        _cells[i].Add(Object.Instantiate(tilePrefab, parentTransform));
                        _cells[i][j].SetActive(false);
                        _cellRenderers[i].Add(_cells[i][j].GetComponent<Renderer>());
                    }
                }
            }
        }
    }
}

