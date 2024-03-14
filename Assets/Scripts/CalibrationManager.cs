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

public class CalibrationManager : MonoBehaviour
{
    [Header("Image related references")]
    [SerializeField] XRReferenceImageLibrary _trackedImagesLibrary;
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] Transform[] _virtualImagesTrs;


    [Header("AR components")]

    [SerializeField] private ARSession _session;
    [SerializeField] private XROrigin _sessionOrigin;
    //[SerializeField] private ARCameraManager cameraManager;

    //[Header("Line Renderer obj")]
   // [SerializeField] private PathRenderer _pathRenderer;
   // ARTrackedImage _currentTrackedImage = null;
    



    bool _isCalibrating = false;

    static Guid[] _trackedImagesIdentifiers;
    int _currentImageIndex = -1;


    // [SerializeField] GameObject _player;
    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        _trackedImagesIdentifiers = new Guid[_trackedImagesLibrary.count];
        for (int i = 0; i < _trackedImagesLibrary.count; i++)
        {
            _trackedImagesIdentifiers[i] = _trackedImagesLibrary[i].guid;
        }

    }


    //void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    //void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    private void Update()
    {
        if (!_isCalibrating)
        {
            ListAllImages();
        }
    }
    void ListAllImages()
    {
        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                      $"{trackedImage.transform.position}");
            textMeshProUGUI.text = trackedImage.referenceImage.name;
            int.TryParse(trackedImage.referenceImage.name, out _currentImageIndex);
            _currentImageIndex--;
            if ( _currentImageIndex != -1 )
            {
                _isCalibrating = true;
                Invoke("Colibrate", 3f);
            }
            
        }
    }
    //void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    //{
        
        //if (!_isCalibrating)
        //{
        //    foreach (ARTrackedImage image in eventArgs.added)
        //    {
        //        if (image.trackingState == TrackingState.Tracking)
        //            _currentTrackedImage = image;
        //    }
        //    if (_currentTrackedImage == null) return;

        //    for (int i = 0; i < _trackedImagesIdentifiers.Length; i++)
        //    {
        //        if (_currentTrackedImage.referenceImage.guid == _trackedImagesIdentifiers[i])
        //            _currentImageIndex = i;
        //    }
        //    Debug.Log(_currentImageIndex);
        //    if (_currentImageIndex == -1) return;
            
        //    Colibrate();
            
        //}
        

    //}
    void Colibrate()
    {
        
        _session.Reset();
        _sessionOrigin.transform.position = _virtualImagesTrs[_currentImageIndex].transform.position;
        _sessionOrigin.transform.rotation = _virtualImagesTrs[_currentImageIndex].transform.rotation;

        //_lineRendererObj.position = _virtualImagesTrs[_currentImageIndex].transform.position;
        //_lineRendererObj.rotation = _virtualImagesTrs[_currentImageIndex].transform.rotation;


    }
}
