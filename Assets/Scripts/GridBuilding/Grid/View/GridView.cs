using System.Collections.Generic;
using Grid.Cells;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;

        private Vector2Int _gridSize;
        private GridViewShower _shower;
        private GridCollection _gridCollection;

        private float _displayAndHideOneCellTime;
        private bool _isShowed = false;

        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; private set; }

        [Inject]
        public void OnConstruct(GridViewShower gridViewShower, GridCollection gridCollection, CellColorChanger cellColorChanger)
        {
            _shower = gridViewShower;
            _gridCollection = gridCollection;
        }

        public void Init(Vector2Int gridSize, Vector3 startPosition, float displayAndHideTime)
        {
            _gridSize = gridSize;
            _displayAndHideOneCellTime = displayAndHideTime / (gridSize.x + gridSize.y) / 2;
            
            _shower.Init(_displayAndHideOneCellTime, _gridCollection);
            
            PrepareCells(startPosition);
        }

        public void CellsLoaded(List<List<Cell>> cells)
        {
            _gridSize = new Vector2Int(cells.Count, cells[0].Count);
            
            PrepareCells(StartPosition);
        }
        
        public void Show()
        {
            if (_isShowed)
                return;

            _shower.Show();

            _isShowed = true;
        }

        public void Hide()
        {
            if (!_isShowed)
                return;

            _shower.Hide();
            _isShowed = false;
        }

        private void PrepareCells(Vector3 startPosition)
        {
            StartPosition = startPosition;
            
            EndPosition = new Vector3(StartPosition.x + 1 * _gridSize.x - 1,
                StartPosition.y,
                StartPosition.z + 1 * _gridSize.y - 1);

            _gridCollection.CreateCollection(_gridSize.x, _gridSize.y, _tilePrefab, 1, StartPosition, transform);
            
            PlaceCells();
        }
        
        private void PlaceCells()
        {
            for (int i = 0; i < _gridCollection.XSize; i++)
            {
                for (int j = 0; j < _gridCollection.YSize; j++)
                {
                    _gridCollection.GetObject(i, j).transform.position = new Vector3(
                        GetCellPositionFromCoordinate(i, 1) + StartPosition.x,
                        StartPosition.y,
                        GetCellPositionFromCoordinate(j, 1) + StartPosition.z);
                }
            }
        }

        [ContextMenu("Clear")]
        private void Clear()
        {
            int childCount = transform.childCount;

            for (int i = childCount; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        private float GetCellPositionFromCoordinate(int coordinate, float scale) =>
            coordinate * scale + scale / 2;
    }
}
