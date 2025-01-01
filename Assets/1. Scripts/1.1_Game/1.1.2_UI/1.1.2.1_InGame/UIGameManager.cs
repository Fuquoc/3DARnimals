using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameManager : Singleton<UIGameManager>
{
    [SerializeField] private FinishGamePopup _popupFinishGame;
    [SerializeField] private PuzzleUIBoard _puzzleUIBoard;
    [SerializeField] private ListPieceUnassembled _listPieceUnassembled;

    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonHint;
    [SerializeField] private Button _buttonResetGame;
    [SerializeField] private Button _buttonPause;

    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _uiAnimalView;
    [SerializeField] private UIPausePopup _uIPausePopup;

    [SerializeField] private Image _originImage;

    private bool isHint;

    private void OnEnable() 
    {
        PuzzleUIBoard.OnFinishPuzzleGame += ShowPopupFinishGame;
        
        _buttonResetGame.onClick.AddListener(OnClickResetGame);
        _buttonBack.onClick.AddListener(OnClickButtonBack);
        _buttonHint.onClick.AddListener(OnClickButtonHint);
        _buttonPause.onClick.AddListener(OnClickButtonPause);
    }

    private void OnDisable()
    {
        PuzzleUIBoard.OnFinishPuzzleGame -= ShowPopupFinishGame;

        _buttonResetGame.onClick.RemoveListener(OnClickResetGame);
        _buttonHint.onClick.RemoveListener(OnClickButtonHint);
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);
        _buttonPause.onClick.RemoveListener(OnClickButtonPause);
    }

    private void ShowPopupFinishGame()
    {
        _popupFinishGame.Open();
    }

    public void ShowUIAnimalView()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while(transform.position.x > -1000)
        {
            yield return null;
            Vector3 position = transform.localPosition;
            position.x -= 1200f * Time.deltaTime;
            transform.localPosition = position;
        }

        gameObject.SetActive(false);
        _uiAnimalView.SetActive(true);
    }

    public void SetOriginImage(Texture2D texture2D)
    {
        _originImage.sprite = Sprite.Create(texture2D, 
                                new Rect(0, 0, texture2D.width, 
                                        texture2D.height), 
                                new Vector2(0.5f, 0.5f));;
    }

    private void OnClickButtonBack()
    {
        GoHome();
    }

    private void OnClickResetGame()
    {
        GameController.Instance.ReStartGame();
    }

    private void OnClickButtonPause()
    {
        Time.timeScale = 0;
        _uIPausePopup.gameObject.SetActive(true);  
    }

    public void GoHome()
    {
        SceneLoader.Instance.LoadSceneAsync(0);
    }

    public void NextLevel()
    {
        GameController.Instance.NextLevel();
    }

    private void OnClickButtonHint()
    {
        if(isHint) return;
        
        var cell = _puzzleUIBoard.GetRandomCellEmptyOrWrongPiece();
        if(cell != null)
        {
            isHint = true;
            cell.Hint();
            _listPieceUnassembled.GetPieceVisual(cell.correctKey).Hint();
        }
    }

    public void FinishHint()
    {
        isHint = false;
    }
}
