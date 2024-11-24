using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUIBoard : MonoBehaviour
{
    public static event Action OnFinishPuzzleGame;

    [SerializeField] private PuzzlePieceCell puzzlePieceCellPrefab;

    [SerializeField] private Transform _layoutContent;
    [SerializeField] private RectTransform _rectBorder;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private PuzzlePieceCell[,] puzzlePieceCells;
    private PuzzlePieceCell _currentCellSelected; //Cell đang được 2 light
    private PuzzleMatrixGenerator _puzzleMatrixGenerator;

    public void AttachScriptGameObject(PuzzleMatrixGenerator puzzleMatrixGenerator)
    {
        _puzzleMatrixGenerator = puzzleMatrixGenerator;
    }

    public void CreatePuzzleUIBoard(int squareSize, int rowLenght, int colLenght)
    {
        UpdateGridViewLayout(squareSize,colLenght);
        UpdateBorder(squareSize,rowLenght,colLenght);
        GeneratePieceCell(rowLenght, colLenght);
    }

    private void UpdateGridViewLayout(int squareSize, int colLenght)
    {
        gridLayoutGroup.cellSize = new Vector2(squareSize, squareSize);
        gridLayoutGroup.spacing = Vector2.zero;
        gridLayoutGroup.constraintCount = colLenght;
    }

    private void UpdateBorder(int squareSize, int rowLenght, int colLenght)
    {
        _rectBorder.sizeDelta = new Vector2(colLenght * squareSize, rowLenght*squareSize);
        _rectBorder.gameObject.SetActive(true);
    }

    private void GeneratePieceCell(int rowLenght, int colLenght) // spawn các ô chứa piece hình ảnh
    {
        puzzlePieceCells = new PuzzlePieceCell[rowLenght, colLenght];
        for(int row = 0; row < rowLenght; row ++)
        {
            for(int col = 0; col < colLenght; col ++)
            {
                var cell = Instantiate(puzzlePieceCellPrefab, _layoutContent);
                cell.Init(this);
                cell.name = $"{puzzlePieceCellPrefab.name}[{row},{col}]";
                puzzlePieceCells[row,col] = cell;
            }
        }
    }

    public bool FindNearestBlankCellPiece(Vector2 from, out PuzzlePieceCell puzzlePieceCell)
    {
        foreach(var cell in puzzlePieceCells)
        {
            if(Vector2.Distance(cell.transform.position, from) < gridLayoutGroup.cellSize.x/2)
            {
                if(cell.IsEmpty())
                {
                    puzzlePieceCell = cell;
                    return true;
                }
            }
        }    

        puzzlePieceCell = null;
        return false;
    }

    public void SelectCellToHighLight(PuzzlePieceCell puzzlePieceCell)
    {
        if(puzzlePieceCell == null || puzzlePieceCell == _currentCellSelected)
            return;

        _currentCellSelected = puzzlePieceCell;
        _currentCellSelected.Highlight();
    }

    public void OnAnotherPuzzlePieceCellAttached()
    {
        foreach(var cell in puzzlePieceCells)
        {
            if(cell.IsEmpty())
            {
                return;
            }
        }

        CheckAllPieceCorrectPosition();
    }

    private void CheckAllPieceCorrectPosition()
    {
        bool _correctAllPiece = true;

        for(int row = 0; row < _puzzleMatrixGenerator.PuzzleMatrixGrid.GetLength(0); row ++)
        {
            for(int col = 0; col < _puzzleMatrixGenerator.PuzzleMatrixGrid.GetLength(0); col ++)
            {
                if(_puzzleMatrixGenerator.PuzzleMatrixGrid[row,col].GetKey() == puzzlePieceCells[row,col].GetPiece().piece.GetKey())
                {
                    Debug.Log($"Ô thứ [{row},{col}] chính xác");
                }
                else 
                {
                    Debug.Log($"Ô thứ [{row},{col}] sai");
                    _correctAllPiece = false;
                }
            }
        }

        if(_correctAllPiece)
        {
            OnFinishPuzzleGame?.Invoke();
        }
    }

    public void EmptyAll()
    {
        foreach(var cell in puzzlePieceCells)
        {
            cell.Empty();
        } 
    }
}
