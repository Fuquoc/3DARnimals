using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterAvatar : MonoBehaviour
{
    [SerializeField] private UINewPlayer _uINewPlayer;
    [SerializeField] private Image _currentAvatar; // Hình ảnh avatar hiện tại
    [SerializeField] private AvatarData avatarData; // Dữ liệu các avatar

    [SerializeField] private Button _buttonComfirm; // Nút xác nhận
    [SerializeField] private Button _nextAvatar;    // Nút chuyển avatar tiếp theo
    [SerializeField] private Button _prevAvatar;    // Nút chuyển avatar trước đó

    private int _currentIndex = 0; // Chỉ số avatar hiện tại

    private void OnEnable()
    {
        _buttonComfirm.onClick.AddListener(OnClickButtonComfirm);
        _nextAvatar.onClick.AddListener(OnNextAvatar);
        _prevAvatar.onClick.AddListener(OnPrevAvatar);

        UpdateAvatarDisplay(); // Hiển thị avatar ban đầu
    }

    private void OnDisable()
    {
        _buttonComfirm.onClick.RemoveListener(OnClickButtonComfirm);
        _nextAvatar.onClick.RemoveListener(OnNextAvatar);
        _prevAvatar.onClick.RemoveListener(OnPrevAvatar);
    }

    private void OnClickButtonComfirm()
    {
        Debug.Log($"Avatar selected: {avatarData.avatarStructs[_currentIndex].avatarName}");
        _uINewPlayer.EnterAvatar(avatarData.avatarStructs[_currentIndex].avatarName);
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }

    private void OnNextAvatar()
    {
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        if (avatarData.avatarStructs.Count == 0) return;

        _currentIndex = (_currentIndex + 1) % avatarData.avatarStructs.Count; // Vòng tròn qua danh sách avatar
        UpdateAvatarDisplay();
    }

    private void OnPrevAvatar()
    {
        SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        if (avatarData.avatarStructs.Count == 0) return;

        _currentIndex = (_currentIndex - 1 + avatarData.avatarStructs.Count) % avatarData.avatarStructs.Count;
        UpdateAvatarDisplay();
    }

    private void UpdateAvatarDisplay()
    {
        if (avatarData.avatarStructs.Count == 0) return;

        _currentAvatar.sprite = avatarData.avatarStructs[_currentIndex].avatarIcon;
    }
}
