using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    [SerializeField] 
    private LineController _lineDrawer;
    
    [SerializeField] 
    private BoxController _box;

    [SerializeField]
    private ScreenController _victoryScreen;
    
    [SerializeField]
    private ScreenController _defeatScreen;

    [SerializeField] 
    private float _speed;

    [SerializeField] 
    private Transform _finishPoint;

    private Vector3[] _points;
    private Vector3 _finishDrawPoint;
    private int _currentPointIndex;
    private float _time;
    private bool _isWin;

    public void StartMoving()
    {
        _points = _lineDrawer.GetPoints();
        _finishDrawPoint = _points[^1];
        StartCoroutine(RopeMoving());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        _isWin = true;
        StopMoving();
        _victoryScreen.EnableScreen();
    }

    private void StopMoving()
    {
        StopCoroutine(RopeMoving());
        _points[_currentPointIndex] = _finishPoint.position;
        _box.DropDown(_points[_currentPointIndex]);
        Debug.Log("DropDown");
    }

    private IEnumerator RopeMoving()
    {
        while (_points[_currentPointIndex] != _finishDrawPoint)
        {
            var nextPoint = (_currentPointIndex + 1) % _points.Length;

            transform.position = Vector3.Lerp(_points[_currentPointIndex], _points[nextPoint], _time);
            _time += Time.deltaTime * _speed / Vector3.Distance(_points[_currentPointIndex], _points[nextPoint]);

            if (_time >= 1f)
            {
                _time = 0f;
                _currentPointIndex = nextPoint;
            }
            yield return null;
        }

        if (!_isWin)
        {
            _defeatScreen.EnableScreen();
        }
        
    }
}