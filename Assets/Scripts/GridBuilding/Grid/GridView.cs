using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        private float _displayAndHideOneCellTime = 1;

        private List<List<GameObject>> _cells;

        private GridViewShower _shower;
        private Vector3 _currentCellPosition;

        private bool _isShowed;

        public Vector3 StartPosition { get; private set; }
        public Vector3 EndPosition { get; private set; }

        private void Awake()
        {
            Clear();
        }
        
        private void Start()
        {
            _isShowed = false;
        }

        [Inject]
        public void OnConstruct(GridViewShower gridViewShower)
        {
            _shower = gridViewShower;
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

        [ContextMenu("Init")]
        public void Init(Vector2 gridSize, Vector3 startPosition, float displayAndHideTime)
        {
            _cells = new();

            StartPosition = startPosition;
            EndPosition = new Vector3(startPosition.x + 1 * gridSize.x - 1,
                StartPosition.y,
                (int)(startPosition.z + 1 * gridSize.y - 1));

            _displayAndHideOneCellTime = displayAndHideTime / (gridSize.x + gridSize.y) / 2;
            _shower.Init(_displayAndHideOneCellTime, _cells);
            
            _currentCellPosition = startPosition;

            for (int i = 0; i < gridSize.x; i++)
            {
                _cells.Add(new());

                _currentCellPosition.x = GetPositionFromCoordinate(i, 1) + startPosition.x;

                for (int j = 0; j < gridSize.y; j++)
                {
                    _currentCellPosition.z = GetPositionFromCoordinate(j, 1) + startPosition.z;

                    _cells[i].Add(Instantiate(_tilePrefab, _currentCellPosition, Quaternion.identity, transform));
                    _cells[i][j].SetActive(false);
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

        private float GetPositionFromCoordinate(int coordinate, float scale)
        {
            return coordinate * scale + scale / 2;
        }
    }
}
