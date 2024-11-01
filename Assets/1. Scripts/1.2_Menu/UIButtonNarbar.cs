using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonNarbar : MonoBehaviour
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private Image _bgImage;

    private int index;
    private UINarbar uINarbar;

    public void Attach(UINarbar uINarbar, int index)
    {
        this.index = index;
        this.uINarbar = uINarbar;
    }

    private void OnEnable() 
    {
        _mainButton.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        uINarbar.ShowScreenByIndex(index);
    }

    public void Select()
    {
        _bgImage.color = Color.white;
    }

    public void UnSelect()
    {
        _bgImage.color = Color.gray;
    }
}
