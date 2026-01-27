using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridViewShower
{
    private List<List<GameObject>> _cells;

    private CoroutinePerformer _coroutineStarter;
    private WaitForSeconds _delay;

    private Coroutine _showCoroutine;
    private Coroutine _hideCoroutine;

    private int _diagonal;
    private int _startRow;
    private int _startCol;

    public GridViewShower(CoroutinePerformer coroutineStarter)
    {
        _coroutineStarter = coroutineStarter;
    }

    public void Init(float delay, List<List<GameObject>> cells)
    {
        _cells = cells;
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
        if (_cells == null)
            Debug.Log(1);
        for (; _diagonal < (_cells.Count + _cells[0].Count) / 2; _diagonal++)
        {
            _startRow = Mathf.Min(_diagonal, _cells.Count - 1);
            _startCol = _diagonal - _startRow;

            for (int k = _startRow, j = _startCol; k >= 0 && j < _cells[0].Count; k--, j++)
            {
                if ((k == _cells.Count - k - 1 && j == _cells[0].Count - j - 1) == false)
                {
                    _cells[_cells.Count - k - 1][_cells[0].Count - j - 1].SetActive(true);
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
            _startRow = Mathf.Min(_diagonal, _cells.Count - 1);
            _startCol = _diagonal - _startRow;

            for (int k = _startRow, j = _startCol; k >= 0 && j < _cells[0].Count; k--, j++)
            {
                if ((k == _cells.Count - k - 1 && j == _cells[0].Count - j - 1) == false)
                {
                    _cells[_cells.Count - k - 1][_cells[0].Count - j - 1].SetActive(false);
                }

                _cells[k][j].SetActive(false);
            }

            yield return _delay;
        }
    }
}
