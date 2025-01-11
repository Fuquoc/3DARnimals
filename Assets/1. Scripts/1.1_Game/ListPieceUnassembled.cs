using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ListPieceUnassembled : MonoBehaviour
{
    [SerializeField] private PuzzlePieceVisual _pieceVisualPrefab;

    [SerializeField] private Transform _dragAreaTemp;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _antiTouchPanel;

    [SerializeField] private PuzzleUIBoard puzzleUIBoard;

    private List<PuzzlePieceVisual> _listPieceVisual;

    private void Awake() 
    {
        _listPieceVisual = new List<PuzzlePieceVisual>();  
    }

    public void CreateUnassembledPiece(Texture2D texture2D, Piece piece) //Unassembled là chưa lắp ghép
    {
        _antiTouchPanel.SetActive(true);

        var obj = Instantiate(_pieceVisualPrefab, _content);
        obj.Init(_dragAreaTemp, puzzleUIBoard);
        obj.CreatePieceVisual(piece, texture2D);
        _listPieceVisual.Add(obj);
    }

    public void RemoveAllUnassembledPiece()
    {
        foreach(var i in _listPieceVisual)
        {
            Destroy(i.gameObject);
        }

        _listPieceVisual.Clear();
    }

    public PuzzlePieceVisual GetPieceVisual(int pieceKey)
    {
        foreach(var vs in _listPieceVisual)
        {
            if(vs.piece.GetKeyRowColumn() == pieceKey)
            {
                return vs;
            }
        }

        return null;
    }

    public void RandomPiece()
    {
        for (int i = _listPieceVisual.Count - 1; i > 0; i--)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, i + 1);
            } while (randomIndex == i);

            PuzzlePieceVisual temp = _listPieceVisual[i];
            _listPieceVisual[i] = _listPieceVisual[randomIndex];
            _listPieceVisual[randomIndex] = temp;
        }

        for (int i = 0; i < _listPieceVisual.Count; i++)
        {
            _listPieceVisual[i].transform.SetSiblingIndex(i);
        }

        _antiTouchPanel.SetActive(false);
    }


}
