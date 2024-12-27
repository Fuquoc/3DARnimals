using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIZoomAnimal : MonoBehaviour
{
    [SerializeField] private Slider _sliderZoom;

    private void OnEnable() 
    {
        _sliderZoom.onValueChanged.AddListener(OnSliderZoomChangeValue);
    }

    private void OnDisable()
    {
        _sliderZoom.onValueChanged.RemoveListener(OnSliderZoomChangeValue);
    }

    private void OnSliderZoomChangeValue(float value)
    {
        Camera.main.fieldOfView = value;
    }
}
