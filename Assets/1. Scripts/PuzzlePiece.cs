using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceVisual : MonoBehaviour
{
    public Piece piece;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteMask _spriteMask;

    [SerializeField] private Texture2D _defaultTexture;
    [SerializeField] private Texture2D _overlayTexture;

    [SerializeField] private GameObject _cirle;

    public void CreatePieceVisual(Piece piece)
    {
        this.piece = piece;
        Texture2D combinedTexture = _defaultTexture;
        if(piece.topEdge == 1)
        {
            GameObject cir = Instantiate(_cirle, transform);
            cir.transform.localPosition = new Vector3(0, 2.5f, 0);

        }
        else if(piece.topEdge == -1)
        {
            combinedTexture = ImageMerger.InsertTextureTop(combinedTexture, _overlayTexture);
        }

        if(piece.rightEdge == 1)
        {
            GameObject cir = Instantiate(_cirle, transform);
            cir.transform.localPosition = new Vector3(2.5f,0, 0);
        } 
        if(piece.rightEdge == -1)
        {
            combinedTexture = ImageMerger.InsertTextureRight(combinedTexture, _overlayTexture);
        }

        if(piece.bottomEdge == 1)
        {
            GameObject cir = Instantiate(_cirle, transform);
            cir.transform.localPosition = new Vector3(0,-2.5f, 0);
        } 
        if(piece.bottomEdge == -1)
        {
            combinedTexture = ImageMerger.InsertTextureBottom(combinedTexture, _overlayTexture);
        }

        if(piece.leftEdge == 1)
        {
            GameObject cir = Instantiate(_cirle, transform);
            cir.transform.localPosition = new Vector3(-2.5f, 0, 0);
        } 
        if(piece.leftEdge == -1)
        {
            combinedTexture = ImageMerger.InsertTextureLeft(combinedTexture, _overlayTexture);
        }
        
        Sprite mergedTexture = Sprite.Create(combinedTexture, new Rect(0, 0, combinedTexture.width, combinedTexture.height), new Vector2(0.5f, 0.5f)); 
        _spriteRenderer.sprite = mergedTexture;
        _spriteMask.sprite = mergedTexture;
    }    
}
