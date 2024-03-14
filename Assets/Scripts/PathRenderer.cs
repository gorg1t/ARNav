using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.UIElements;

public class PathRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    public float minDistance = 0.1f; 
    private List<Vector3> pathPoints = new List<Vector3>();

    [SerializeField] private GameObject _transformPerson;

    public bool _canRender = false;
    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = false;
    }
    void UpdatePath()
    {
        pathPoints.Add(new Vector3(transform.position.x, transform.position.y-0.8f, transform.position.z));
        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = pathPoints.Count;

        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pathPoints[i]);
        }
    }

    void LateUpdate()
    {
        if (_canRender && (pathPoints.Count == 0 || (transform.position - pathPoints[pathPoints.Count - 1]).sqrMagnitude > minDistance * minDistance))
        {
            UpdatePath();
        }
    }


    public void StartRender()
    {
        transform.position = _transformPerson.transform.position;
        transform.rotation = _transformPerson.transform.rotation;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 0;
        pathPoints = new List<Vector3>();
        _canRender = true;
    }
    public void StopRender () 
    {
        _canRender = false;
    }
}
