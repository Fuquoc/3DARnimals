using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterName : MonoBehaviour
{
    [SerializeField] private TMP_InputField tMP_InputFieldName;
    [SerializeField] private Button _buttonComfirm;
    [SerializeField] private UINewPlayer uINewPlayer;

    private void OnEnable()
    {
        _buttonComfirm.onClick.AddListener(OnClickButtonComfirm);
        tMP_InputFieldName.onValueChanged.AddListener(OnInputChange);

        tMP_InputFieldName.text = string.Empty;
        OnInputChange(string.Empty);
    }

    private void OnDisable()
    {
        _buttonComfirm.onClick.RemoveListener(OnClickButtonComfirm);
    }

    private void OnClickButtonComfirm()
    {
        uINewPlayer.EnterName(tMP_InputFieldName.text);
    }

    private void OnInputChange(string newValue)
    {
        if(newValue == string.Empty)
        {
            _buttonComfirm.interactable = false;
        }
        else
        {
            _buttonComfirm.interactable = true;
        }
    }
}
