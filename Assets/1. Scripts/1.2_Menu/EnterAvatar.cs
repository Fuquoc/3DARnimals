using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterAvatar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpAge;
    [SerializeField] private Button _buttonComfirm;

    private void OnEnable()
    {
        _buttonComfirm.onClick.AddListener(OnClickButtonComfirm);
    }

    private void OnDisable()
    {
        _buttonComfirm.onClick.RemoveListener(OnClickButtonComfirm);
    }

    private void OnClickButtonComfirm()
    {
        
    }
}
