using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Vector2 _gridSize;

        private List<List<GameObject>> _cells;
        private List<List<MeshRenderer>> _cellMeshRenderers;

        private float _displayAndHideOneCellTime;
        private GridViewShower _shower;
        private Vector3 _currentCellPosition;

        private bool _isShowed = false;

        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; private set; }

        private void Awake()
        {
            Clear();
        }

        [Inject]
        public void OnConstruct(GridViewShower gridViewShower)
        {
            _shower = gridViewShower;
        }

        public void Init(Vector2 gridSize, Vector3 startPosition, float displayAndHideTime)
        {
            _gridSize = gridSize;
            StartPosition = startPosition;
            
            EndPosition = new Vector3(startPosition.x + 1 * gridSize.x - 1,
                StartPosition.y,
                startPosition.z + 1 * gridSize.y - 1);
            _displayAndHideOneCellTime = displayAndHideTime / (gridSize.x + gridSize.y) / 2;

            CreateGrid();
            _shower.Init(_displayAndHideOneCellTime, _cells);
        }

        [ContextMenu("CreateGrid")]
        public void CreateGrid()
        {
            _cells = new();
            _cellMeshRenderers = new();
            _currentCellPosition = StartPosition;

            for (int i = 0; i < _gridSize.x; i++)
            {
                _cells.Add(new());
                _cellMeshRenderers.Add(new());

                _currentCellPosition.x = GetPositionFromCoordinate(i, 1) + StartPosition.x;

                for (int j = 0; j < _gridSize.y; j++)
                {
                    _currentCellPosition.z = GetPositionFromCoordinate(j, 1) + StartPosition.z;

                    _cells[i].Add(Instantiate(_tilePrefab, _currentCellPosition, Quaternion.identity, transform));
                    _cellMeshRenderers[i].Add(_cells[i][j].GetComponent<MeshRenderer>());

                    _cells[i][j].SetActive(false);
                }
            }
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

        [ContextMenu("Clear")]
        private void Clear()
        {
            int childCount = transform.childCount;

            for (int i = childCount; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        private float GetPositionFromCoordinate(int coordinate, float scale)
        {
            return coordinate * scale + scale / 2;
        }
    }
}
