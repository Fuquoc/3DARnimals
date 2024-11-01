using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUIBoard : MonoBehaviour
{
    [SerializeField] private PuzzlePieceCell puzzlePieceCellPrefab;

    [SerializeField] private Transform _layoutContent;
    [SerializeField] private RectTransform _rectBorder;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private List<PuzzlePieceCell> puzzlePieceCells;

    private PuzzlePieceCell _currentCellSelected; //Cell đang được 2 light

    private void Awake() 
    {
        puzzlePieceCells = new List<PuzzlePieceCell>();
    }

    public void CreatePuzzleUIBoard(int squareSize, int rowLenght, int colLenght)
    {
        UpdateGridViewLayout(squareSize,colLenght);
        UpdateBorder(squareSize,rowLenght,colLenght);
        for(int row = 0; row < rowLenght; row ++)
        {
            for(int col = 0; col < colLenght; col ++)
            {
                var cell = Instantiate(puzzlePieceCellPrefab, _layoutContent);
                puzzlePieceCells.Add(cell);
            }
        }
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
}
