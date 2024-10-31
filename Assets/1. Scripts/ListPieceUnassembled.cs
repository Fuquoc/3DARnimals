using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPieceUnassembled : MonoBehaviour
{
    [SerializeField] private PuzzlePieceVisual _pieceVisualPrefab;

     [SerializeField] private Transform _dragAreaTemp;
    [SerializeField] private Transform _content;

    [SerializeField] private PuzzleUIBoard puzzleUIBoard;

    private List<PuzzlePieceVisual> _listPieceVisual;

    private void Awake() 
    {
        _listPieceVisual = new List<PuzzlePieceVisual>();  
    }

    public void CreateUnassembledPiece(Texture2D texture2D, Piece piece)
    {
        var obj = Instantiate(_pieceVisualPrefab, _content);
        obj.Init(_dragAreaTemp, puzzleUIBoard);
        obj.CreatePieceVisual(piece, texture2D);
        _listPieceVisual.Add(obj);
    }
}
