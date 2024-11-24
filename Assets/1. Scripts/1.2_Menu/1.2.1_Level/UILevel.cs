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

    private void OnEnable() 
    {
        _levelButton.onClick.AddListener(OnClickLevel);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(OnClickLevel);
    }

    public void Init(LevelConfigData levelConfigData)
    {
        _levelConfigData = levelConfigData;

        _lock.SetActive(_levelConfigData.isLock);

        _originImage.sprite = Sprite.Create(_levelConfigData.originText, 
                                            new Rect(0, 0, _levelConfigData.originText.width, 
                                                _levelConfigData.originText.height), 
                                            new Vector2(0.5f, 0.5f));

        _textNameLevel.text = $"Màn chơi {_levelConfigData.level}";
        _textNumberPiece.text = $"{_levelConfigData.sizeMatrix * _levelConfigData.sizeMatrix} Mảnh";
    }

    private void OnClickLevel()
    {
        LevelSelection.Instance.PlayLevel(_levelConfigData);
    }
}
