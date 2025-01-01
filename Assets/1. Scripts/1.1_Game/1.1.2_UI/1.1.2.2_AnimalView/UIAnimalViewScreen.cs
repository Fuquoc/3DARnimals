using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimalViewScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonAR;
    [SerializeField] private Button _buttonGallery;
    [SerializeField] private Button _buttonVideo;

    [SerializeField] private GameObject _galleryUI;

    private void OnEnable() 
    {
        _buttonBack.onClick.AddListener(OnClickButtonBack);
        _buttonAR.onClick.AddListener(OnClickButtonAR);
        _buttonGallery.onClick.AddListener(OnClickButtonGallery);
        _buttonVideo.onClick.AddListener(OnClickButtonVideo);
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);
        _buttonAR.onClick.RemoveListener(OnClickButtonAR);
        _buttonGallery.onClick.RemoveListener(OnClickButtonGallery);
        _buttonVideo.onClick.RemoveListener(OnClickButtonVideo);
    }

    private void OnClickButtonBack()
    {
        SceneLoader.Instance.LoadSceneAsync(0);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }

    private void OnClickButtonAR()
    {
        SceneLoader.Instance.LoadSceneAsync(2);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }

    private void OnClickButtonGallery()
    {
        _galleryUI.SetActive(true);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }

    private void OnClickButtonVideo()
    {
        Application.OpenURL(LevelSelection.Instance._currentLevelSelect.levelAnimalImageGallery.urlVideo);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }
}
