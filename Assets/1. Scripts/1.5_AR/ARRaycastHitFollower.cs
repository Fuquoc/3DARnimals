using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ARRaycastHitFollower : MonoBehaviour
{
    public static event Action OnTouchInIndicator;

    public GameObject _indicator;
    public ARRaycastManager arRaycastManager;
    public LayerMask _indicatorLayerMask;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARRaycastHit _currenrtARRaycastHit; 

    private bool _isShowIndicator = true;


    void Update()
    {
        if(_isShowIndicator == false) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinBounds))
        {
            _currenrtARRaycastHit = hits[0];

            Pose hitPose = _currenrtARRaycastHit.pose;

            if (!(_currenrtARRaycastHit.trackable is ARPlane arPlane))
                    return;

            _indicator.transform.position = hitPose.position;
            _indicator.SetActive(true);
        }
        else
        {
            _indicator.SetActive(false);
        }

        TouchInIndicator();
    }

    private void TouchInIndicator()
    {
        if(Input.GetMouseButtonDown(0))
        {           
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, _indicatorLayerMask))
            {
                OnTouchInIndicator?.Invoke();
            }
        }
    }

    private void ActiveIndicator()
    {
        _isShowIndicator = true;
        _indicator.SetActive(true);
    }

    private void DeActiveIndicator()
    {
        _isShowIndicator = false;
        _indicator.SetActive(false);
    }
}
