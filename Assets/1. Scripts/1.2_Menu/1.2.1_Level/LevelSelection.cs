using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : Singleton<LevelSelection>
{
    [SerializeField] private LevelConfig levelConfig;

    public LevelConfigData _currentLevelSelect;

    public void PlayLevel(LevelConfigData levelConfigData)
    {
        _currentLevelSelect = levelConfigData;

        SceneLoader.Instance.LoadSceneAsync(1);
    }

    public void GoToNextLevel()
    {
        foreach(var level in levelConfig.Levels)
        {
            if(level.level == _currentLevelSelect.level + 1)
            {
                _currentLevelSelect = level;
                break;
            }
        }

        SceneLoader.Instance.LoadSceneAsync(1);
    }
}
