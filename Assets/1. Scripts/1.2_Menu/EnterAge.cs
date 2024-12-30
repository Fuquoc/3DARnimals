using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterAge : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpAge;
    [SerializeField] private Button _buttonComfirm;
    [SerializeField] private UINewPlayer uINewPlayer;
    [SerializeField] private Button _buttonNextAge;
    [SerializeField] private Button _buttonPreviousAge;

    private int currentAge = 18; // Default starting age
    private const int minAge = 10; // Minimum allowed age
    private const int maxAge = 100; // Maximum allowed age

    private void OnEnable()
    {
        _buttonComfirm.onClick.AddListener(OnClickButtonComfirm);
        _buttonNextAge.onClick.AddListener(OnClickButtonNextAge);
        _buttonPreviousAge.onClick.AddListener(OnClickButtonPreviousAge);
        UpdateAgeText();
    }

    private void OnDisable()
    {
        _buttonComfirm.onClick.RemoveListener(OnClickButtonComfirm);
        _buttonNextAge.onClick.RemoveListener(OnClickButtonNextAge);
        _buttonPreviousAge.onClick.RemoveListener(OnClickButtonPreviousAge);
    }

    private void OnClickButtonComfirm()
    {
        uINewPlayer.EnterAge(currentAge); // Example: Assume SetPlayerAge exists in UINewPlayer
        Debug.Log($"Age confirmed: {currentAge}");
    }

    private void OnClickButtonNextAge()
    {
        if (currentAge < maxAge)
        {
            currentAge++;
            UpdateAgeText();
        }
    }

    private void OnClickButtonPreviousAge()
    {
        if (currentAge > minAge)
        {
            currentAge--;
            UpdateAgeText();
        }
    }

    private void UpdateAgeText()
    {
        tmpAge.text = currentAge.ToString();
    }
}
