using Grid;
using System.Collections;
using UnityEngine;

public class GridViewShower
{
    private GridCollection _gridCollection;

    private CoroutinePerformer _coroutineStarter;
    private WaitForSeconds _delay;

    private Coroutine _showCoroutine;
    private Coroutine _hideCoroutine;

    private int _diagonal;
    private int _startRow;
    private int _startCol;
    private int _currentCellIndexX;
    private int _currentCellIndexY;

    public GridViewShower(CoroutinePerformer coroutineStarter)
    {
        _coroutineStarter = coroutineStarter;
    }

    public void Init(float delay, GridCollection gridCollection)
    {
        _gridCollection = gridCollection;
        _delay = new WaitForSeconds(delay);
    }

    public void Show()
    {
        if (_hideCoroutine != null)
            _coroutineStarter.StopCoroutine(_hideCoroutine);

        _showCoroutine = _coroutineStarter.StartCoroutine(ShowFromAngleToAngle());
    }

    public void Hide()
    {
        if (_showCoroutine != null)
            _coroutineStarter.StopCoroutine(_showCoroutine);

        _hideCoroutine = _coroutineStarter.StartCoroutine(HideFromAngleToAngle());
    }

    private IEnumerator ShowFromAngleToAngle()
    {
        for (; _diagonal < (_gridCollection.XSize + _gridCollection.YSize) / 2; _diagonal++)
        {
            _startRow = Mathf.Min(_diagonal, _gridCollection.XSize - 1);
            _startCol = _diagonal - _startRow;

            for (int k = _startRow, j = _startCol; k >= 0 && j < _gridCollection.YSize; k--, j++)
            {
                if ((k == _gridCollection.XSize - k - 1 && j == _gridCollection.YSize - j - 1) == false)
                {
                    _currentCellIndexX = _gridCollection.XSize - k - 1;
                    _currentCellIndexY = _gridCollection.YSize - j - 1;
                    _gridCollection.GetObject(_currentCellIndexX, _currentCellIndexY).SetActive(true);
                }

                _gridCollection.GetObject(k, j).SetActive(true);
            }

            yield return _delay;
        }
    }

    private IEnumerator HideFromAngleToAngle()
    {
        for (; _diagonal >= 0; _diagonal--)
        {
            _startRow = Mathf.Min(_diagonal, _gridCollection.XSize - 1);
            _startCol = _diagonal - _startRow;

            for (int k = _startRow, j = _startCol; k >= 0 && j < _gridCollection.YSize; k--, j++)
            {
                if ((k == _gridCollection.XSize - k - 1 && j == _gridCollection.YSize - j - 1) == false)
                {
                    _gridCollection.GetObject(_gridCollection.XSize - k - 1, _gridCollection.YSize - j - 1)
                        .SetActive(false);
                }

                _gridCollection.GetObject(k, j).SetActive(false);
            }

            yield return _delay;
        }
    }
}