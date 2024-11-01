using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTextureCutter
{
    public static Color transparentColor = new Color(0,0,0,0);

    public static Texture2D CreatePieceTextureWithPivot(Texture2D originTexture, int squareSize, int radiusCircle, Vector2 centerInOrigin, Piece piece)
    {
        int textureWidth = squareSize + radiusCircle * 2;
        int textureHeight = squareSize + radiusCircle * 2;

        int startX = (int)(centerInOrigin.x - textureWidth / 2); // vị trí của pixel 0,0 so với texture origin 
        int startY = (int)(centerInOrigin.y - textureHeight / 2);

        int minXSquare = radiusCircle;
        int minYSquare = radiusCircle;
        int maxXSquare = radiusCircle + squareSize;
        int maxYSquare = radiusCircle + squareSize;

        Vector2 centerEdgeTop = new Vector2(radiusCircle+squareSize/2, radiusCircle+squareSize);
        Vector2 centerEdgeRight = new Vector2(radiusCircle+squareSize, radiusCircle+squareSize/2);
        Vector2 centerEdgeBottom = new Vector2(radiusCircle+squareSize/2, radiusCircle);
        Vector2 centerEdgeLeft = new Vector2(radiusCircle, radiusCircle+squareSize/2);

        Texture2D cutPiece = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);

        for (int x = 0; x <= textureWidth; x++)
        {
            for (int y = 0; y <= textureHeight; y++)
            {
                if(x >= minXSquare && x <= maxXSquare && y >= minYSquare && y <= maxYSquare)
                {
                    Color color = default;

                    bool top = GetColorConCave(x, y, piece.topEdge, centerEdgeTop, ref color);
                    bool right = GetColorConCave(x, y, piece.rightEdge, centerEdgeRight, ref color);
                    bool bottom = GetColorConCave(x, y, piece.bottomEdge, centerEdgeBottom, ref color);
                    bool left = GetColorConCave(x, y, piece.leftEdge, centerEdgeLeft, ref color);

                    if((top || right || bottom || left) == false)
                    {
                        color = originTexture.GetPixel(startX + x, startY + y);
                    }

                    cutPiece.SetPixel(x, y, color);
                }
                else 
                {

                    if(x >= maxXSquare)
                    {
                        FillColorConVex(x, y, piece.rightEdge, centerEdgeRight);
                    }

                    if(y >= maxYSquare)
                    {
                        FillColorConVex(x, y, piece.topEdge, centerEdgeTop);
                    }

                    if(y <= minYSquare)
                    {
                        FillColorConVex(x, y, piece.bottomEdge, centerEdgeBottom);
                    }

                    if(x <= minXSquare)
                    {
                        FillColorConVex(x, y, piece.leftEdge, centerEdgeLeft);
                    }
                }
            }

            bool GetColorConCave(int x, int y,int pieceEdge, Vector2 centerEdge, ref Color color)
            {
                if(pieceEdge == -1)
                {
                    if(Vector2.Distance(centerEdge, new Vector2(x, y)) < radiusCircle)
                    {
                        color = transparentColor;
                        return true;
                    }
                }
                color = default;
                return false;
            }

            void FillColorConVex(int x, int y, int pieceEdge, Vector2 centerEdge)
            {
                if(pieceEdge == 1)
                {
                    if(Vector2.Distance(centerEdge, new Vector2(x, y)) >= radiusCircle)
                    {
                        Color color = transparentColor;
                        cutPiece.SetPixel(x, y, color);
                    }
                    else
                    {
                        Color color = originTexture.GetPixel(startX + x, startY + y);
                        cutPiece.SetPixel(x, y, color);
                    }
                }
                else 
                {
                    Color color = transparentColor;
                    cutPiece.SetPixel(x, y, color);
                }
            }
        }

        cutPiece.Apply();

        return cutPiece;
    }
}
