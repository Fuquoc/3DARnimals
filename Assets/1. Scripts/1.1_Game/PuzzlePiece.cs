using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePieceVisual : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Piece piece;

    private Transform _lastParent;
    private Transform _dragAreaTemp;
    private RectTransform _rectTransform;

    private int realWidth;
    private int realHeight;

    private PuzzleUIBoard PuzzleUIBoard;

    [SerializeField] private Image _imagePiece;

    public PuzzlePieceCell puzzlePieceCellAttach;

    private void Start() 
    {
        _rectTransform = GetComponent<RectTransform>();    
    }

    public void Init(Transform DragArea, PuzzleUIBoard puzzleUIBoard)
    {
        _dragAreaTemp = DragArea;
        this.PuzzleUIBoard = puzzleUIBoard;
    }

    public void CreatePieceVisual(Piece piece, Texture2D texture2D)
    {
        realWidth = texture2D.width;
        realHeight = texture2D.height;

        Sprite mergedTexture = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f)); 
        _imagePiece.sprite = mergedTexture;

        this.piece = piece;

        SetLastParent(transform.parent);
    }

    public void RevertPieceToRealSize()
    {
        RectTransform rect = (RectTransform)transform;

        rect.sizeDelta = new Vector2(realWidth, realHeight);
    }

    private void SetLastParent(Transform parent)
    {
        _lastParent = parent;
        transform.parent = parent;
    }

    public void BackToListPieceUnassembled()
    {
        puzzlePieceCellAttach?.Empty();
        puzzlePieceCellAttach = null;
        transform.parent = _lastParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        
        PuzzleUIBoard.FindNearestBlankCellPiece(eventData.position, out var cell);
        PuzzleUIBoard.SelectCellToHighLight(cell);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent = _dragAreaTemp;

        RevertPieceToRealSize();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(PuzzleUIBoard.FindNearestBlankCellPiece(eventData.position, out var cell))
        {
            cell.AttachThePiece(this);
        }
        else if(puzzlePieceCellAttach != null)
        {
            if(Vector2.Distance(this.transform.position, puzzlePieceCellAttach.transform.position) > 
                (_rectTransform.sizeDelta.x * _rectTransform.lossyScale.x +_rectTransform.sizeDelta.y * _rectTransform.lossyScale.y)/4)
            {
                BackToListPieceUnassembled();
            }
            else
            {
                puzzlePieceCellAttach.AttachThePiece(this);
            }
        }
        else 
        {
            BackToListPieceUnassembled();
        }
    }

    public void Hint()
    {
        BackToListPieceUnassembled();
        GetComponent<FlickerSprite>().On();
    }

    public void FinishHint()
    {
        GetComponent<FlickerSprite>().Off();
    }
}
