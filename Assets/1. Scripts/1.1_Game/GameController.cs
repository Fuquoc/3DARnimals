using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PuzzleMatrixGenerator _puzzleMatrixGenerator;
    [SerializeField] private CreatePieceFromMatrix _createPieceFromMatrix;
    [SerializeField] private UIGameManager _uIGameManager;
    [SerializeField] private AnimalSpawner _animalSpawner;

    private void OnEnable() 
    {
        _uIGameManager.OnClickResetGameButton.AddListener(ReStartGame);
    }

    private void Start() 
    {
        _puzzleMatrixGenerator.SetSize(LevelSelection.Instance._currentLevelSelect.sizeMatrix);
        _createPieceFromMatrix.SetOriginTexture(LevelSelection.Instance._currentLevelSelect.originText);
        _uIGameManager.SetOriginImage(LevelSelection.Instance._currentLevelSelect.originText);
        _animalSpawner.SetAnimal(LevelSelection.Instance._currentLevelSelect.animal);

        _puzzleMatrixGenerator.StartGenerater();
    }

    public void ReStartGame()
    {
        _puzzleMatrixGenerator.ReGenerateGrid();
    }
}
