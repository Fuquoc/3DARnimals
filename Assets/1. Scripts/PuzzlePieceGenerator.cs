using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Piece
{
    public int[] edges = new int[4]; // 0 = phẳng, -1 là lõm, 1 là lồi

    public Piece topPiece;
    public Piece rightPiece;
    public Piece bottomPiece;
    public Piece leftPiece;

    public Piece(int top, int right, int bottom, int left)
    {
        edges[0] = top;
        edges[1] = right;
        edges[2] = bottom;
        edges[3] = left;
    }

    public int topEdge => edges[0];
    public int rightEdge => edges[1];
    public int bottomEdge => edges[2];
    public int leftEdge => edges[3];

    public int SetTop(int value) => edges[0] = value;
    public int SetRight(int value) => edges[1] = value;
    public int SetBottom(int value) => edges[2] = value;
    public int SetLeft(int value) => edges[3] = value;

    public void SetEdge(int top, int right, int bottom, int left)
    {
        SetTop(top);
        SetRight(right);
        SetBottom(bottom);
        SetLeft(left);
    }

    public string PrintLog()
    {
        string result = "";
        foreach(var e in edges)
        {
            result += $"[{e}]";
        }

        return result;
    }

    public int GetKey()
    {
        return topEdge * 1000 + rightEdge * 100 + bottomEdge * 10 + leftEdge;
    }
}

public class PuzzlePieceGenerator : MonoBehaviour
{
    private List<Piece> allPieces = new List<Piece>();

    private Dictionary<int, PuzzlePieceVisual> puzzlePiecePrefabs = new Dictionary<int, PuzzlePieceVisual>();   

    public PuzzlePieceVisual _pieceVisualPrefab;

    private void Start() 
    {
        StartCoroutine(GenerateAllPieces());    
    }

    IEnumerator GenerateAllPieces()
    {
        // Các loại cạnh có thể là 0 (phẳng), 1 (lõm), 2 (lồi)
        int[] edgeTypes = { -1, 0, 1 };
        
        foreach (int top in edgeTypes)
        {
            foreach (int right in edgeTypes)
            {
                foreach (int bottom in edgeTypes)
                {
                    foreach (int left in edgeTypes)
                    {
                        yield return new WaitForSeconds(0.1f);
                        Piece piece = new Piece(top, right, bottom, left);
                        allPieces.Add(piece);

                        CreatePieceVisual(piece);
                    }
                }
            }
        }
    }

    private void CreatePieceVisual(Piece piece)
    {
        var obj = Instantiate(_pieceVisualPrefab, transform);
        obj.CreatePieceVisual(piece);

        obj.gameObject.SetActive(false);

        puzzlePiecePrefabs.Add(piece.GetKey(),obj);
    }

    public PuzzlePieceVisual GetPiece(int top, int right, int bottom, int left)
    {
        int key = top * 1000 + right * 100 + bottom * 10 + left;
        return puzzlePiecePrefabs[key];
    }

    public PuzzlePieceVisual GetPiece(Piece pieceData)
    {
        if(puzzlePiecePrefabs.ContainsKey(pieceData.GetKey()))
        {
            return puzzlePiecePrefabs[pieceData.GetKey()];
        }
        else
        {
            return null;
        }
    }
}
