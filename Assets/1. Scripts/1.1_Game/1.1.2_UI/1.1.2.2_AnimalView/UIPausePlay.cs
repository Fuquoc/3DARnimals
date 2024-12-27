using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePlay : MonoBehaviour
{
    [SerializeField] private GameObject _playIcon;
    [SerializeField] private GameObject _pauseIcon;

    public void ShowPlay(bool isShow)
    {
        _playIcon.SetActive(isShow);
        _pauseIcon.SetActive(isShow == false);
    }
}
