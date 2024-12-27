using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimalViewScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonAR;

    private void OnEnable() 
    {
        _buttonBack.onClick.AddListener(OnClickButtonBack);
        _buttonAR.onClick.AddListener(OnClickButtonAR);
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);
        _buttonAR.onClick.RemoveListener(OnClickButtonAR);
    }

    private void OnClickButtonBack()
    {
        SceneLoader.Instance.LoadSceneAsync(0);
    }

    private void OnClickButtonAR()
    {
        SceneLoader.Instance.LoadSceneAsync(2);
    }
}
