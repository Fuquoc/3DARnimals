using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : Singleton<GameController>
{
    [SerializeField] private PuzzleMatrixGenerator _puzzleMatrixGenerator;
    [SerializeField] private CreatePieceFromMatrix _createPieceFromMatrix;
    [SerializeField] private UIGameManager _uIGameManager;
    [SerializeField] private AnimalSpawner _animalSpawner;

    [Header("DEBUG OPTION")]
    [SerializeField] private bool DebugUseTestLevel;
    [SerializeField] private int levelIndex;
    [SerializeField] private LevelConfig levelConfig;

    public float TimePlayGame;

    private void Start() 
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        LevelConfigData levelConfigData = null;
        if(DebugUseTestLevel == false)
        {
            levelConfigData = LevelSelection.Instance._currentLevelSelect;
        }
        else 
        {
            levelConfigData = levelConfig.Levels[levelIndex];
        }


        _puzzleMatrixGenerator.SetSize(levelConfigData.sizeMatrix);
        _createPieceFromMatrix.SetOriginTexture(levelConfigData.originText);
        _uIGameManager.SetOriginImage(levelConfigData.originText);
        _animalSpawner.SetAnimal(levelConfigData.animal);

        _puzzleMatrixGenerator.StartGenerater();
    }

    public void ReStartGame()
    {
        TimePlayGame = 0;
        _puzzleMatrixGenerator.ReGenerateGrid();
    }

    public void NextLevel()
    {
        LevelSelection.Instance.GoToNextLevel();
    }

    public void View3D()
    {
        _animalSpawner.SpawnAnimal();
    }

    private void Update() 
    {
        TimePlayGame += Time.deltaTime;   
    }
}
