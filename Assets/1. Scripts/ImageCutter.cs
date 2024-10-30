using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCutter : MonoBehaviour
{
    public Texture2D sourceImage; // Ảnh gốc cần cắt
    public int squareSize = 100;
    public int radiusCircle = 25;
    public Vector2 centerInOrigin;
    public Piece piece;
    Color transparentColor = new Color(0,0,0,0);

    private void Awake() 
    {
        centerInOrigin.x = sourceImage.width/2;
        centerInOrigin.y = sourceImage.height/2;
    }

    [ContextMenu("ABC")]
    public void Test()
    {
System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
stopwatch.Start();
    Texture2D centerPiece = CreatePieceTextureWithPivot();
            
stopwatch.Stop();
Debug.Log(stopwatch.ElapsedTicks + "tick" + " " + stopwatch.ElapsedMilliseconds +"ms");
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(centerPiece, new Rect(0, 0, centerPiece.width, centerPiece.height), new Vector2(0.5f, 0.5f));
    }

    Texture2D CreatePieceTextureWithPivot()
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

        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                if(x >= minXSquare && x <= maxXSquare && y >= minYSquare && y <= maxYSquare)
                {
                    Color color = default;

                    bool top = GetColor(x, y, piece.topEdge, centerEdgeTop, ref color);
                    bool right = GetColor(x, y, piece.rightEdge, centerEdgeRight, ref color);
                    bool bottom = GetColor(x, y, piece.bottomEdge, centerEdgeBottom, ref color);
                    bool left = GetColor(x, y, piece.leftEdge, centerEdgeLeft, ref color);

                    if((top || right || bottom || left) == false)
                    {
                        color = sourceImage.GetPixel(startX + x, startY + y);
                    }

                    cutPiece.SetPixel(x, y, color);
                }
            }

            bool GetColor(int x, int y,int pieceEdge, Vector2 centerEdge, ref Color color)
            {
                if(piece.topEdge == -1)
                {
                    if(Vector2.Distance(centerEdgeTop, new Vector2(x, y)) < radiusCircle)
                    {
                        color = transparentColor;
                        return true;
                    }
                }
                color = default;
                return false;
            }
        }

        // Vector2 center = new Vector2(100, 50);

        // for(int x = 100; x <= 125; x++)
        // {
        //     for (int y = 0; y <= 100; y++)
        //     {
        //         if(Vector2.Distance(center, new Vector2(x, y)) > 25)
        //         {
        //             Color color = new Color(0,0,0,0);
        //             cutPiece.SetPixel(x, y, color);
        //         }
        //         else
        //         {
        //             Color color = sourceImage.GetPixel(startX + x, startY + y);
        //             cutPiece.SetPixel(x, y, color);
        //         }

        //     }
        // }

        // for(int x = 0; x <= 100; x++)
        // {
        //     for (int y = 0; y <= 100; y++)
        //     {
        //         if(Vector2.Distance(center, new Vector2(x, y)) < 25)
        //         {
        //             Color color = new Color(0,0,0,0);
        //             cutPiece.SetPixel(x, y, color);
        //         }
        //     }
        // }



        // for (int x = (int)center.x; x < ; x++)
        // {
        //     for (int y = 0; y < squareSize; y++)
        //     {

        //     }
        // }

        cutPiece.Apply();

        return cutPiece;
    }
}
