using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBodyPartInfomation : MonoBehaviour
{
    [SerializeField] private Button eyeButton;
    [SerializeField] private Button noseButton;
    [SerializeField] private Button mouthButton;
    [SerializeField] private Button earButton;
    [SerializeField] private Button brainButton;
    [SerializeField] private Button footButton;
    [SerializeField] private Button leatherButton;
    [SerializeField] private Button tailButton;

    [SerializeField] private UIPausePlay uIPausePlay;

    private LevelConfigData levelData;
    private bool _isPlay;
    private string currentButton;

    private void Start() 
    {
        levelData = LevelSelection.Instance._currentLevelSelect;    
    }

    private void OnEnable() 
    {
        if (eyeButton != null)
            eyeButton.onClick.AddListener(OnClickButtonEye);

        if (noseButton != null)
            noseButton.onClick.AddListener(OnClickButtonNose);

        if (mouthButton != null)
            mouthButton.onClick.AddListener(OnClickButtonMouth);

        if (earButton != null)
            earButton.onClick.AddListener(OnClickButtonEar);

        if (brainButton != null)
            brainButton.onClick.AddListener(OnClickButtonBrain);

        if (footButton != null)
            footButton.onClick.AddListener(OnClickButtonFoot);

        if (leatherButton != null)
            leatherButton.onClick.AddListener(OnClickButtonLeather);

        if (tailButton != null)
            tailButton.onClick.AddListener(OnClickButtonTail);
    }

    private void OnDisable()
    {
        if (eyeButton != null)
            eyeButton.onClick.RemoveListener(OnClickButtonEye);

        if (noseButton != null)
            noseButton.onClick.RemoveListener(OnClickButtonNose);

        if (mouthButton != null)
            mouthButton.onClick.RemoveListener(OnClickButtonMouth);

        if (earButton != null)
            earButton.onClick.RemoveListener(OnClickButtonEar);

        if (brainButton != null)
            brainButton.onClick.RemoveListener(OnClickButtonBrain);

        if (footButton != null)
            footButton.onClick.RemoveListener(OnClickButtonFoot);

        if (leatherButton != null)
            leatherButton.onClick.RemoveListener(OnClickButtonLeather);

        if (tailButton != null)
            tailButton.onClick.RemoveListener(OnClickButtonTail);
    }

    private void OnClickButtonEye()
    {
        PlaySound(levelData.levelSound.eyeAudioClip, eyeButton.transform);
    }

    private void OnClickButtonNose()
    {
        PlaySound(levelData.levelSound.noseAudioClip, noseButton.transform);
    }

    private void OnClickButtonMouth()
    {
        PlaySound(levelData.levelSound.mouthAudioClip, mouthButton.transform);
    }

    private void OnClickButtonEar()
    {
        PlaySound(levelData.levelSound.earAudioClip, earButton.transform);
    }

    private void OnClickButtonBrain()
    {
        PlaySound(levelData.levelSound.brainAudioClip, brainButton.transform);
    }

    private void OnClickButtonFoot()
    {
        PlaySound(levelData.levelSound.footAudioClip, footButton.transform);
    }

    private void OnClickButtonLeather()
    {
        PlaySound(levelData.levelSound.leatherAudioClip, leatherButton.transform);
    }

    private void OnClickButtonTail()
    {
        PlaySound(levelData.levelSound.tailAudioClip, tailButton.transform);
    }

    private void PlaySound(AudioClip clip, Transform button)
    {
        uIPausePlay.transform.position = button.position;
        uIPausePlay.transform.parent = button;

        if(currentButton != button.name)
        {
            currentButton = button.name;

            if (SoundController.Instance != null && clip != null)
            {
                SoundController.Instance.PlaySound(clip);
            }

            _isPlay = true;
            uIPausePlay.ShowPlay(true);
        }
        else 
        {
            if(_isPlay)
            {
                _isPlay = false;

                if (SoundController.Instance != null && clip != null)
                {
                    SoundController.Instance.PauseSoundEffect();
                }

                uIPausePlay.ShowPlay(_isPlay);
            }
            else
            {
                _isPlay = true;

                if (SoundController.Instance != null && clip != null)
                {
                    SoundController.Instance.ResumeSoundEffect();
                }

                uIPausePlay.ShowPlay(_isPlay);
            }
        }
    }
}
