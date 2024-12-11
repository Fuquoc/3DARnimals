using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishGamePopup : MonoBehaviour
{
    [SerializeField] private UIGameManager _UIGameManager;

    [SerializeField] private TextMeshProUGUI _textLevelName;
    [SerializeField] private TextMeshProUGUI _textPieceCount;
    [SerializeField] private TextMeshProUGUI _textTime;

    [SerializeField] private Button _buttonRestartGame; 
    [SerializeField] private Button _buttonHome; 
    [SerializeField] private Button _buttonNextLevel; 
    [SerializeField] private Button _buttonView3D;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        InitSelf();
        AddButtonListeners();    
    }

    private void OnDisable()
    {
        RemoveButtonListeners();
    }

    private void InitSelf()
    {
        LevelConfigData lvconfigData = LevelSelection.Instance._currentLevelSelect;

        InitText(lvconfigData.level, lvconfigData.sizeMatrix, 360);
    }

    // Hàm khởi tạo nội dung chữ
    public void InitText(int level, int sizeMatrix, float timeInSeconds)
    {
        _textLevelName.text = $"Màn chơi {level}";
        _textPieceCount.text = $"{sizeMatrix * sizeMatrix} Mảnh";
        _textTime.text = $"Time: {FormatTime(timeInSeconds)}";
    }

    // Hàm định dạng thời gian thành "MM:SS"
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:D2}:{seconds:D2}";
    }

    public void AddButtonListeners()
    {
        // Gắn sự kiện cho nút Restart Game
        _buttonRestartGame.onClick.RemoveAllListeners();
        _buttonRestartGame.onClick.AddListener(OnClickButtonRestartGame);

        // Gắn sự kiện cho nút Home
        _buttonHome.onClick.RemoveAllListeners();
        _buttonHome.onClick.AddListener(OnClickButtonHome);

        // Gắn sự kiện cho nút Next Level
        _buttonNextLevel.onClick.RemoveAllListeners();
        _buttonNextLevel.onClick.AddListener(OnClickButtonNextLevel);

        // Gắn sự kiện cho nút View 3D
        _buttonView3D.onClick.RemoveAllListeners();
        _buttonView3D.onClick.AddListener(OnClickButtonView3D);
    }

    private void RemoveButtonListeners()
    {
        _buttonRestartGame.onClick.RemoveAllListeners();
        _buttonHome.onClick.RemoveAllListeners();
        _buttonNextLevel.onClick.RemoveAllListeners();
        _buttonView3D.onClick.RemoveAllListeners();
    }

    // Hàm sự kiện khi nhấn nút Restart Game
    private void OnClickButtonRestartGame()
    {
        Debug.Log("Restart Game button clicked!");
        GameController.Instance.ReStartGame();
        Close();
    }

    // Hàm sự kiện khi nhấn nút Home
    private void OnClickButtonHome()
    {
        Debug.Log("Home button clicked!");
        _UIGameManager.GoHome();
        Close();
    }

    // Hàm sự kiện khi nhấn nút Next Level
    private void OnClickButtonNextLevel()
    {
        Debug.Log("Next Level button clicked!");
        _UIGameManager.NextLevel();
        Close();
    }

    // Hàm sự kiện khi nhấn nút View 3D
    private void OnClickButtonView3D()
    {
        Debug.Log("View 3D button clicked!");
        _UIGameManager.ShowUIAnimalView();
        GameController.Instance.View3D();
        Close();
    }
}
