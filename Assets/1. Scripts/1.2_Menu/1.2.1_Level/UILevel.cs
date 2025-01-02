using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    private LevelConfigData _levelConfigData;

    [SerializeField] private Image _originImage;
    [SerializeField] private TextMeshProUGUI _textNameLevel;
    [SerializeField] private TextMeshProUGUI _textNumberPiece;

    [SerializeField] private Button _levelButton;

    [SerializeField] private GameObject _lock;

    [SerializeField] private GameObject[] starUIArray;

    private bool isLock;

    private void OnEnable() 
    {
        _levelButton.onClick.AddListener(OnClickLevel);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(OnClickLevel);
    }

    public void Init(LevelConfigData levelConfigData, PlayerLevelData playerLevelData)
    {
        _levelConfigData = levelConfigData;

        _lock.SetActive(playerLevelData.pass == false && playerLevelData.isCurrentLevel == false);
        // _levelButton.enabled = playerLevelData.pass || playerLevelData.isCurrentLevel;
        isLock = playerLevelData.pass == false && playerLevelData.isCurrentLevel == false;

        _originImage.sprite = Sprite.Create(_levelConfigData.originText, 
                                            new Rect(0, 0, _levelConfigData.originText.width, 
                                                _levelConfigData.originText.height), 
                                            new Vector2(0.5f, 0.5f));

        _textNameLevel.text = $"Màn chơi {_levelConfigData.level}";
        _textNumberPiece.text = $"{_levelConfigData.sizeMatrix * _levelConfigData.sizeMatrix}";

        ShowStar(playerLevelData.star);
    }

    private void OnClickLevel()
    {
        if(isLock)
        {
            PopupManager.Instance.ShowPopup(1, "Đồng ý", "Đồng ý", "Thông báo", "Bạn phải hoàn thành màn chơi trước đó");
        }
        else 
        {
            LevelSelection.Instance.PlayLevel(_levelConfigData);
            SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        }
    }

    private void ShowStar(int star)
    {
        foreach(var ui in starUIArray)
        {
            ui.SetActive(false);
        }

        starUIArray[star].SetActive(true);
    }
}
