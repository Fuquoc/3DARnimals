using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class SceneLoader : Singleton<SceneLoader>
{
    public Slider progressBar; // Slider để hiển thị tiến trình, nếu có

    public Action<int> OnLoadScene;

    public void LoadSceneAsync(int sceneIndex)
    {
        OnLoadScene?.Invoke(sceneIndex);
        StartCoroutine(LoadScene(sceneIndex));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        // Bắt đầu load scene bất đồng bộ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        
        // Đảm bảo scene không tự động chuyển khi tải xong
        asyncOperation.allowSceneActivation = false;

        // Kiểm tra tiến trình tải
        while (!asyncOperation.isDone)
        {
            // Nếu có thanh tiến trình, cập nhật nó
            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            }

            // Khi tiến trình đạt tới 90% (0.9), scene đã sẵn sàng để chuyển
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
