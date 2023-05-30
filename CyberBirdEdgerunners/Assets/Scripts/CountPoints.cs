using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using Data;

public class CountPoints : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private string _preFix;
    private string _pointsString;
    [SerializeField] private float _countPointsStepTime;
    private bool _countingPoints;
    private Text _text;
    private Timer _timer;
    private int _index = 0;
    [SerializeField] private UnityEvent _onCountPoint;
    [SerializeField] private UnityEvent _onStart;

    [SerializeField] private DataInt _pointData;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _timer = GetComponent<Timer>();
        _onStart.Invoke();
        CountPointsAmount();
    }

    public void GetPoints()
    {
        _points = _pointData.Variable;
    }

    public void GetPointsTime()
    {
        _points = (int)Time.timeSinceLevelLoad;
    }

    private void CountPointsFunction()
    {
        if (!_countingPoints)
            return;
        _onCountPoint?.Invoke();
        _timer.StartTimer(_countPointsStepTime, CountPointsFunction);
        _text.text += _pointsString[_index];
        _index += 1;
        if (_index >= _pointsString.Length)
            _countingPoints = false;
    }

    public void CountPointsAmount()
    {
        _pointsString = _points + _preFix;
        _text.text = "";
        _countingPoints = true;
        _index = 0;
        float waitTime = 2;
        _timer.StartTimer(waitTime, CountPointsFunction);
    }
}
