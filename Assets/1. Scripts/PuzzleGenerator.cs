using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PuzzleGenerator : MonoBehaviour
{    
    private const int SIZE = 4;
    private Piece[,] puzzleGrid = new Piece[SIZE, SIZE];

    [SerializeField] private PuzzlePieceGenerator puzzlePieceGenerator;

    public void Start()
    {
        GenerateTempPiece();
        AasignNeighborPiece();
    }

    private void GenerateTempPiece()
    {
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Piece newPiece = new Piece(-2, -2, -2 , -2);
                puzzleGrid[row,col] = newPiece;
            }
        }
    }

    private void AasignNeighborPiece()
    {
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Piece piece = puzzleGrid[row,col];

                piece.topPiece = GetPiece(row-1,col);
                piece.rightPiece = GetPiece(row,col+1);
                piece.bottomPiece = GetPiece(row+1,col);
                piece.leftPiece = GetPiece(row,col-1);
            }
        }
    }

    private Piece GetPiece(int row, int col)
    {
        // Kiểm tra giới hạn của bảng
        if (row >= 0 && row < puzzleGrid.GetLength(0) && col >= 0 && col < puzzleGrid.GetLength(1))
        {
            if (puzzleGrid[row, col] != null)
            {
                return puzzleGrid[row, col];
            }
        }

        return null;
    }

    [ContextMenu("ABC")]
    public void GeneratePuzzleGrid()
    {
        // Sử dụng thuật toán random để tạo 4x4 mảnh ghép khớp nhau
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                if(row == 0 || row == SIZE -1 || col == 0 || col == SIZE - 1)
                {
                    RandomPieceBound(row, col);
                }
                else 
                {
                    RandomNormalPiece(row,col);
                }
            }
        }

        DebugLog();
    }

    [ContextMenu("Show Grid")]
    public void DebugLog()
    {
        string log = "";
        for(int row = 0; row < SIZE; row++)
        {
            for(int col = 0; col < SIZE; col++)
            {
                if(puzzleGrid[row,col] != null)
                {
                    log += puzzleGrid[row,col].PrintLog() + "  ";
                }
                else 
                {
                    log += "[][][][]" + "  ";
                }
            }

            log += "\n";
        }

        Debug.Log(log);
    }

    public Piece RandomPieceBound(int row, int col)
    {
        if(row != 0 && row != SIZE -1 && col != 0 && col != SIZE - 1) return null;

        Debug.Log($"Random bound at: {row}-{col}");

        int topFixed = row == 0 ? 0 : -2;
        int rightFixed = col == 3 ? 0 : -2;
        int bottomFixed = row == 3 ? 0 : -2;
        int leftFixed = col == 0 ? 0 : -2;

        var neighbor = GetEdgeOfNeighbor(row, col);

        topFixed = neighbor.top != -2 ? - neighbor.top : topFixed;
        rightFixed = neighbor.right != -2 ? - neighbor.right : rightFixed;
        bottomFixed = neighbor.bottom != -2 ? - neighbor.bottom : bottomFixed;
        leftFixed = neighbor.left != -2 ? - neighbor.left : leftFixed;

        List<Piece> validPieces = GetListPieceValid(topFixed, rightFixed, bottomFixed, leftFixed);

        // Chọn một mảnh hợp lệ ngẫu nhiên từ các mảnh phù hợp
        Piece selectedPiece = validPieces[Random.Range(0, validPieces.Count)];

        puzzleGrid[row,col].SetEdge(selectedPiece.topEdge, selectedPiece.rightEdge, selectedPiece.bottomEdge, selectedPiece.leftEdge);

        return selectedPiece;
    }

    public Piece RandomNormalPiece(int row, int col)
    {
        int topFixed = -2;
        int rightFixed = -2;
        int bottomFixed = -2;
        int leftFixed = -2;

        var neighbor = GetEdgeOfNeighbor(row, col);

        topFixed = neighbor.top != -2 ? - neighbor.top : topFixed;
        rightFixed = neighbor.right != -2 ? - neighbor.right : rightFixed;
        bottomFixed = neighbor.bottom != -2 ? - neighbor.bottom : bottomFixed;
        leftFixed = neighbor.left != -2 ? - neighbor.left : leftFixed;

        List<Piece> validPieces = GetListPieceValid(topFixed, rightFixed, bottomFixed, leftFixed);

        // Chọn một mảnh hợp lệ ngẫu nhiên từ các mảnh phù hợp
        Piece selectedPiece = validPieces[Random.Range(0, validPieces.Count)];

        puzzleGrid[row,col].SetEdge(selectedPiece.topEdge, selectedPiece.rightEdge, selectedPiece.bottomEdge, selectedPiece.leftEdge);

        return selectedPiece;  
    }

    public List<Piece> GetListPieceValid(int topFixed, int rightFixed, int bottomFixed, int leftFixed)
    {
        Debug.Log(topFixed + " " + rightFixed + " " +   bottomFixed + " " +  leftFixed);
        List<Piece> listPieceValid = new List<Piece>();
        
        int[] edgeTypesTop = topFixed != -2 ? new int[] { topFixed } : new int[] { -1, 1 };
        int[] edgeTypesRight = rightFixed != -2 ? new int[] { rightFixed } : new int[] { -1, 1 };
        int[] edgeTypesBottom = bottomFixed != -2 ? new int[] { bottomFixed } : new int[] { -1, 1 };
        int[] edgeTypesLeft = leftFixed != -2 ? new int[] { leftFixed } : new int[] { -1, 1 };

        foreach (int top in edgeTypesTop)
        {
            foreach (int right in edgeTypesRight)
            {
                foreach (int bottom in edgeTypesBottom)
                {
                    foreach (int left in edgeTypesLeft)
                    {
                        listPieceValid.Add(new Piece(top, right, bottom, left));
                    }
                }
            }
        }

        return listPieceValid;
    }

    private (int top, int right, int bottom, int left) GetEdgeOfNeighbor(int row, int col)
    { 
        Piece targetPiece = puzzleGrid[row, col];
        int top = targetPiece.topPiece == null ? -2 : targetPiece.topPiece.bottomEdge;
        int right = targetPiece.rightPiece == null ? -2 : targetPiece.rightPiece.leftEdge;
        int bottom = targetPiece.bottomPiece == null ? -2 : targetPiece.bottomPiece.topEdge;
        int left = targetPiece.leftPiece == null ? -2 : targetPiece.leftPiece.rightEdge;

        return (top, right, bottom, left);
    }

    // private (int top, int right, int bottom, int left) GetValuePieceAround(int row, int col)
    // {
    //     int top = GetBottomEdgeOfPieceTop(row, col);        
    //     int right = -2;        
    //     int bottom = -2;        
    //     int left = -2;

    //     return surroundingPieces;
    // }

    bool IsPieceFit(Piece piece, int row, int col)
    {
        return true;
        // Kiểm tra cạnh trên
        if (row > 0 && puzzleGrid[row - 1, col] != null)
        {
            if (puzzleGrid[row - 1, col].edges[2] + piece.edges[0] != 3) return false;
        }

        // Kiểm tra cạnh trái
        if (col > 0 && puzzleGrid[row, col - 1] != null)
        {
            if (puzzleGrid[row, col - 1].edges[1] + piece.edges[3] != 3) return false;
        }

        return true;
    }

    void DisplayPuzzleGrid()
    {
        // Hàm này chỉ để kiểm tra kết quả
        for (int row = 0; row < SIZE; row++)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Piece piece = puzzleGrid[row, col];
                Debug.Log($"Piece at ({row}, {col}): Top={piece.edges[0]}, Right={piece.edges[1]}, Bottom={piece.edges[2]}, Left={piece.edges[3]}");
            }
        }
    }
}