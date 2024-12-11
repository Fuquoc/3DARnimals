using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerSprite : MonoBehaviour
{
    [SerializeField] private float flickerSpeed = 2f;

    
    private Image _mainImage;
    private Color startColor;

    private void Awake() 
    {
        _mainImage = GetComponent<Image>();  
    }

    private void Start() 
    {
        startColor = _mainImage.color;

        Off();
    }

    public void On()
    {
        enabled = true;
        _mainImage.color = startColor;
    }

    public void Off()
    {
        enabled = false;
        _mainImage.color = startColor;
    }

    private void Update() 
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed)) * 0.5f;
        _mainImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
    }
}
