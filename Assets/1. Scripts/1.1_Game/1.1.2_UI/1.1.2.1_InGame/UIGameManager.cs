using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    public UnityEvent OnClickResetGameButton;

    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonHint;
    [SerializeField] private Button _buttonResetGame;

    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _uiAnimalView;

    [SerializeField] private Image _originImage;

    private void OnEnable() 
    {
        PuzzleUIBoard.OnFinishPuzzleGame += ShowUIAnimalView;
        
        _buttonResetGame.onClick.AddListener(OnClickResetGame);
        _buttonBack.onClick.AddListener(OnClickButtonBack);
    }

    private void OnDisable()
    {
        PuzzleUIBoard.OnFinishPuzzleGame -= ShowUIAnimalView;

        _buttonResetGame.onClick.RemoveListener(OnClickResetGame);
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);
    }

    private void ShowUIAnimalView()
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
        SceneLoader.Instance.LoadSceneAsync(0);
    }

    private void OnClickResetGame()
    {
        OnClickResetGameButton?.Invoke();
    }
}
