using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARUIController : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;

    private void OnEnable() 
    {
        _buttonBack.onClick.AddListener(OnClickButtonBack);   
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);   
    }

    private void OnClickButtonBack()
    {
        SceneLoader.Instance.LoadSceneAsync(0);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }
}
