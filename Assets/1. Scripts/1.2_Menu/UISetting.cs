using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private Image imageAvatar;
    [SerializeField] private TextMeshProUGUI textNamePlayer;
    [SerializeField] private TextMeshProUGUI textAgePlayer;

    [SerializeField] private Button buttonClearData;

    [SerializeField] private AvatarData avatarData;    

    private void OnEnable()
    {   
        UpdateSaveSetting();

        musicSlider.onValueChanged.AddListener(OnSliderMusicChange);
        soundSlider.onValueChanged.AddListener(OnSliderSoundChange);

        buttonClearData.onClick.AddListener(OnClickClearData);

        UpdateInfo();
    }

    private void OnDisable()
    {
        musicSlider.onValueChanged.RemoveListener(OnSliderMusicChange);
        soundSlider.onValueChanged.RemoveListener(OnSliderSoundChange);

        buttonClearData.onClick.RemoveListener(OnClickClearData);
    }

    private void UpdateInfo()
    {
        var playerdata = JsonDataHandler.LoadData<PlayerData>();
        textNamePlayer.text = playerdata.name;
        textAgePlayer.text = playerdata.age.ToString() + " tuổi";

        foreach(var avt in avatarData.avatarStructs)
        {
            if(avt.avatarName == playerdata.avatarName)
            {
                imageAvatar.sprite = avt.avatarIcon;
            }
        }
    }

    private void UpdateSaveSetting()
    {
        var cur = Player.Instance.GetPlayerData;

        soundSlider.value = cur.soundVolumeSetting;
        musicSlider.value = cur.musicVolumeSetting;
        SoundController.Instance.SetSoundEffectVolume(cur.soundVolumeSetting);
        SoundController.Instance.SetMusicVolume(cur.musicVolumeSetting);
    }

    private void OnSliderSoundChange(float value)
    {
        SoundController.Instance.SetSoundEffectVolume(value);

        Player.Instance.SaveSoundSetting(value);
    }

    private void OnSliderMusicChange(float value)
    {
        SoundController.Instance.SetMusicVolume(value);

        Player.Instance.SaveMusicSetting(value);
    }

    private void OnClickClearData()
    {
        Player.Instance.ClearAllData();
    }
}
