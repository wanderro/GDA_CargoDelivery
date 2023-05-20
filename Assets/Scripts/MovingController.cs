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
    private float _speed;

    private Vector3[] _points;
    private Vector3 _finishPoint;
    private int _currentPointIndex;
    private float _time;

    public void StartMoving()
    {
        _points = _lineDrawer.GetPoints();
        _finishPoint = _points[^1];
        StartCoroutine(RopeMoving());
    }

    public void StopMoving()
    {
        StopCoroutine(RopeMoving());
        _box.DropDown(_points[_currentPointIndex]);
    }

    private IEnumerator RopeMoving()
    {
        while (_points[_currentPointIndex] != _finishPoint)
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
    }
}