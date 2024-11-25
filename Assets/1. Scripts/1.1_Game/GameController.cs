using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    [SerializeField] private PuzzleMatrixGenerator _puzzleMatrixGenerator;
    [SerializeField] private CreatePieceFromMatrix _createPieceFromMatrix;
    [SerializeField] private UIGameManager _uIGameManager;
    [SerializeField] private AnimalSpawner _animalSpawner;

    [Header("DEBUG OPTION")]
    [SerializeField] private bool DebugUseTestLevel;
    [SerializeField] private int levelIndex;
    [SerializeField] private LevelConfig levelConfig;

    private void OnEnable() 
    {
        _uIGameManager.OnClickResetGameButton.AddListener(ReStartGame);
    }

    private void Start() 
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
        _puzzleMatrixGenerator.ReGenerateGrid();
    }
}
