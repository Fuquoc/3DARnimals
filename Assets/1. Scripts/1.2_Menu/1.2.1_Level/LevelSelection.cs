using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : Singleton<LevelSelection>
{
    public LevelConfigData _currentLevelSelect;

    public void PlayLevel(LevelConfigData levelConfigData)
    {
        _currentLevelSelect = levelConfigData;

        SceneLoader.Instance.LoadSceneAsync(1);
    }
}
