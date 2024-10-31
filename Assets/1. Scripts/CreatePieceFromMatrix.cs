using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreatePieceFromMatrix : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private PuzzleMatrixGenerator puzzleMatrixGenerator;
    [SerializeField] private ListPieceUnassembled listPieceUnassembled;
    [SerializeField] private PuzzleUIBoard puzzleUIBoard;
    
    [SerializeField] private Texture2D originTexture;

    private const float k_CircleSize_Per_SquareSize_Ratio = 0.15f;

    public void OnGenerateMatrixPieceSuccess()
    {
        Piece[,] matrix = puzzleMatrixGenerator.PuzzleGrid;
        StartCoroutine(CreateListPiece(matrix));
    }

    private IEnumerator CreateListPiece(Piece[,] matrixPiece)
    {
        int rowLenght = matrixPiece.GetLength(0);
        int colLenght = matrixPiece.GetLength(1);

        int squareSize = originTexture.height / rowLenght;
        int radiusCircle = (int)(squareSize * k_CircleSize_Per_SquareSize_Ratio);

        int startX = (originTexture.width - originTexture.height)/2;

        for (int row = 0; row < rowLenght; row++)
        {
            for (int col = 0; col < colLenght; col++)
            {
                yield return new WaitForSeconds(0.05f);
                Piece piece = matrixPiece[row,col];
                int x = startX + squareSize * col + squareSize/2;
                int y =  originTexture.height - ((squareSize * row) + squareSize/2);
                Texture2D texturePiece = PieceTextureCutter.CreatePieceTextureWithPivot(originTexture, squareSize, radiusCircle, new Vector2(x,y), piece);

                listPieceUnassembled.CreateUnassembledPiece(texturePiece, piece);
            }
        }

        puzzleUIBoard.CreatePuzzleUIBoard(squareSize, rowLenght, colLenght);
    }
}
