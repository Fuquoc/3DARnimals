using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePopup : MonoBehaviour
{
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonHome;
    [SerializeField] private Button buttonReplayGame;

    [SerializeField] private UIGameManager uIGameManager;

    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private Image[] startImage;

    [SerializeField] private Sprite sprStarBlack;
    [SerializeField] private Sprite sprStarYellow;

    private void OnEnable() 
    {
        // Gán sự kiện cho các nút
        buttonContinue.onClick.AddListener(OnClickButtonContinue);
        buttonHome.onClick.AddListener(OnClickButtonHome);
        buttonReplayGame.onClick.AddListener(OnClickButtonRestartGame);

        UpdateData();
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện để tránh lỗi trùng lặp
        buttonContinue.onClick.RemoveListener(OnClickButtonContinue);
        buttonHome.onClick.RemoveListener(OnClickButtonHome);
        buttonReplayGame.onClick.RemoveListener(OnClickButtonRestartGame);
    }

    private void UpdateData() 
    {
        LevelConfigData lvconfigData = LevelSelection.Instance._currentLevelSelect;

        int star = 0;
        float timePlayGame = GameController.Instance.TimePlayGame;

        if(timePlayGame <= lvconfigData.levelStarConfig.maxSecondToTake3Star)
        {
            star = 3;
        }
        else if(timePlayGame <= lvconfigData.levelStarConfig.maxSecondToTake2Star)
        {
            star = 2;
        }
        else if(timePlayGame <= lvconfigData.levelStarConfig.maxSecondToTake1Star)
        {
            star = 1;
        }

        for (int i = 0; i < startImage.Length; i++)
        {
            if (i < star)
            {
                startImage[i].sprite = sprStarYellow; // Đổi thành ngôi sao vàng
            }
            else
            {
                startImage[i].sprite = sprStarBlack; // Đổi thành ngôi sao đen
            }
        }

        textTime.text = $"Thời gian: {FormatTime(timePlayGame)}";
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return $"{minutes:D2}:{seconds:D2}";
    }

    // Hàm sự kiện khi nhấn nút Continue
    private void OnClickButtonContinue()
    {
        Debug.Log("Continue button clicked!");
        Close();
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
        uIGameManager.GoHome();
        Close();
    }

    private void Close()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
