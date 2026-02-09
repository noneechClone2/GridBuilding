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
        private CellColorChanger _cellColorChanger;

        private float _displayAndHideOneCellTime;
        private bool _isShowed = false;

        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; private set; }

        [Inject]
        public void OnConstruct(GridViewShower gridViewShower, GridCollection gridCollection, CellColorChanger cellColorChanger)
        {
            _shower = gridViewShower;
            _gridCollection = gridCollection;
            _cellColorChanger = cellColorChanger;
        }

        public void Init(Vector2Int gridSize, Vector3 startPosition, float displayAndHideTime)
        {
            _gridSize = gridSize;
            _displayAndHideOneCellTime = displayAndHideTime / (gridSize.x + gridSize.y) / 2;
            
            PrepareCells(startPosition, EndPosition);
        }

        public void OnCellsLoaded(List<List<Cell>> cells)
        {
            _gridSize = new Vector2Int(cells.Count, cells[0].Count);
            
            PrepareCells(StartPosition, EndPosition);
            print(1);
            _cellColorChanger.CellCollectionLoaded(cells);
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

        private void PrepareCells(Vector3 startPosition, Vector3 endPosition)
        {
            StartPosition = startPosition;
            
            EndPosition = new Vector3(startPosition.x + 1 * _gridSize.x - 1,
                StartPosition.y,
                startPosition.z + 1 * _gridSize.y - 1);

            _gridCollection.CreateCollection(_gridSize.x, _gridSize.y, _tilePrefab, 1, StartPosition, transform);
            PlaceCells();
            _shower.Init(_displayAndHideOneCellTime, _gridCollection);
        }
        
        private void PlaceCells()
        {
            for (int i = 0; i < _gridCollection.XSize; i++)
            {
                for (int j = 0; j < _gridCollection.YSize; j++)
                {
                    _gridCollection.GetObject(i, j).transform.position = new Vector3(
                        GetPositionFromCoordinate(i, 1) + StartPosition.x,
                        StartPosition.y,
                        GetPositionFromCoordinate(j, 1) + StartPosition.z);
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

        private float GetPositionFromCoordinate(int coordinate, float scale) =>
            coordinate * scale + scale / 2;
    }
}
