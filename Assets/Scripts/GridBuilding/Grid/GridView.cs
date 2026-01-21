using Builders;
using Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private int _sizeX, _sizeY;
        [SerializeField] private float _tileSize = 1;
        [SerializeField] private float _displayAndHideTime = 1;

        [field: SerializeField] public Vector3 StartPosition { get; private set; }

        private List<List<GameObject>> _cells;

        private Vector3 _currentCellPosition;
        private WaitForSeconds _delay;
        private Coroutine _showCoroutine;
        private Coroutine _hideCoroutine;

        private bool _isShowed;
        private int _diagonal;
        private int _startRow;
        private int _startCol;

        public Vector3 EndPosition { get; private set; }

        private void Start()
        {
            _startRow = 0;
            _startCol = 0;

            EndPosition = new Vector3(StartPosition.x + _tileSize * _sizeX - 1, StartPosition.y, StartPosition.z + _tileSize * _sizeY - 1);

            Clear();
            Draw();

            _delay = new WaitForSeconds(_displayAndHideTime / (_sizeX + _sizeY) / 2);
            _isShowed = false;

        }
        public void Show()
        {
            if (_isShowed)
                return;

            if (_hideCoroutine != null)
                StopCoroutine(_hideCoroutine);

            _showCoroutine = StartCoroutine(nameof(ShowFromAngleToAngle));
            _isShowed = true;
        }

        public void Hide()
        {
            if (!_isShowed)
                return;

            if (_showCoroutine != null)
                StopCoroutine(_showCoroutine);

            _hideCoroutine = StartCoroutine(nameof(HideFromAngleToAngle));
            _isShowed = false;
        }

        [ContextMenu("Draw")]
        private void Draw()
        {
            _cells = new();

            _currentCellPosition = StartPosition;

            for (int i = 0; i < _sizeX; i++)
            {
                _cells.Add(new());

                _currentCellPosition.x = GetPositionFromCoordinate(i) + StartPosition.x;

                for (int j = 0; j < _sizeY; j++)
                {
                    _currentCellPosition.z = GetPositionFromCoordinate(j) + StartPosition.z;

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

        private float GetPositionFromCoordinate(int coordinate)
        {
            return coordinate * _tileSize + _tileSize / 2;
        }

        private IEnumerator ShowFromAngleToAngle()
        {
            for (; _diagonal < (_sizeX + _sizeY) / 2; _diagonal++)
            {
                _startRow = Mathf.Min(_diagonal, _sizeX - 1);
                _startCol = _diagonal - _startRow;

                for (int k = _startRow, j = _startCol; k >= 0 && j < _sizeY; k--, j++)
                {
                    if ((k == _sizeX - k - 1 && j == _sizeY - j - 1) == false)
                    {
                        _cells[_sizeX - k - 1][_sizeY - j - 1].SetActive(true);
                    }

                    _cells[k][j].SetActive(true);
                }

                yield return _delay;
            }
        }

        private IEnumerator HideFromAngleToAngle()
        {
            for (; _diagonal >= 0; _diagonal--)
            {
                _startRow = Mathf.Min(_diagonal, _sizeX - 1);
                _startCol = _diagonal - _startRow;

                for (int k = _startRow, j = _startCol; k >= 0 && j < _sizeY; k--, j++)
                {
                    if ((k == _sizeX - k - 1 && j == _sizeY - j - 1) == false)
                    {
                        _cells[_sizeX - k - 1][_sizeY - j - 1].SetActive(false);
                    }

                    _cells[k][j].SetActive(false);
                }

                yield return _delay;
            }
        }
    }
}
