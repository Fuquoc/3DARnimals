using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelScreen : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;

    [SerializeField] private UILevel _uILevelPrefab;
    [SerializeField] private RectTransform _content;

    private void Start() 
    {
        InitLevel();    
    }

    private void InitLevel()
    {
        foreach(var levelData in _levelConfig.Levels)
        {
            var ui = Instantiate(_uILevelPrefab, _content);
            ui.Init(levelData);
        }
    }
}
