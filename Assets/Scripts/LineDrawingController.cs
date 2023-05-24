using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _rope;

    [SerializeField] 
    private MovingController _movingController;
    
    private LineRenderer _lineRenderer;
    private Vector3 _startPosition;
    private bool _isLineDrawn;
    

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _startPosition = new Vector3(_rope.transform.position.x,
            _rope.transform.position.y, _rope.transform.position.z);
        
        _lineRenderer.SetPosition(0, _startPosition);
        _lineRenderer.positionCount = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isLineDrawn)
        {
            Drawing(1);
        }

        if (Input.GetMouseButton(0) && !_isLineDrawn)
        {
            Drawing(_lineRenderer.positionCount);
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    private void Drawing(int index)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            _lineRenderer.positionCount++;

            var currentPosition = hitInfo.point;
            currentPosition.z = _startPosition.z;

            _lineRenderer.SetPosition(index, currentPosition);
        }
    }

    private void StopDrawing()
    {
        _isLineDrawn = true;
        _movingController.StartMoving();
    }

    public Vector3[] GetPoints()
    {
        var positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        return positions ?? positions;
    }
}
