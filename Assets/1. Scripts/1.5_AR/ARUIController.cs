using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARUIController : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonScale11;
    [SerializeField] private Button _buttonScale110;
    [SerializeField] private Button _buttonDelete;

    [SerializeField] private ObjectSpawner objectSpawner;

    private void OnEnable() 
    {
        _buttonBack.onClick.AddListener(OnClickButtonBack);
        _buttonScale11.onClick.AddListener(OnClickButtonScale11);
        _buttonScale110.onClick.AddListener(OnClickButtonScale110);
        _buttonDelete.onClick.AddListener(OnClickButtonDelete);
    }

    private void OnDisable()
    {
        _buttonBack.onClick.RemoveListener(OnClickButtonBack);
        _buttonScale11.onClick.RemoveListener(OnClickButtonScale11);
        _buttonScale110.onClick.RemoveListener(OnClickButtonScale110);
        _buttonDelete.onClick.RemoveListener(OnClickButtonDelete);
    }

    private void OnClickButtonBack()
    {
        SceneLoader.Instance.LoadSceneAsync(0);
        // SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
    }

    private void OnClickButtonScale11()
    {
        // Thực hiện scale vật thể về 1x1
        Debug.Log("Scale 1:1");
        // SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        // Gọi chức năng scale object tùy theo logic bạn muốn
        objectSpawner.SetScale(1); // Đặt scale 1:1
    }

    private void OnClickButtonScale110()
    {
        // Thực hiện scale vật thể về 1x10
        Debug.Log("Scale 1:10");
        // SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        objectSpawner.SetScale(1/10f); // Đặt scale 1:10
    }

    private void OnClickButtonDelete()
    {
        Debug.Log("Delete object");
        // SoundController.Instance.PlaySoundEffectOneShot(SoundFXType.Button);
        objectSpawner.DeleteCurrentObject(); // Hàm giả định xóa vật thể đang được chọn
    }
}
