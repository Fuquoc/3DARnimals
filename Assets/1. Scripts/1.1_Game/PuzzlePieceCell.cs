using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EPieceCellState
{
    empty,
    attached,
}

public class PuzzlePieceCell : MonoBehaviour
{
    [SerializeField] private Image _bgColor;

    public int correctKey {get; set;}

    private PuzzleUIBoard _puzzleUIBoard;
    private PuzzlePieceVisual _currentPuzzlePieceVisual;
    private EPieceCellState ePieceCellState = EPieceCellState.empty;
    private Coroutine _fadeOutRoutine;

    public void Init(PuzzleUIBoard puzzleUIBoard)
    {
        _puzzleUIBoard = puzzleUIBoard;
    }

    public void AttachThePiece(PuzzlePieceVisual puzzlePieceVisual)
    {
        puzzlePieceVisual.puzzlePieceCellAttach?.Empty();

        ePieceCellState = EPieceCellState.attached;
        puzzlePieceVisual.puzzlePieceCellAttach = this;
        puzzlePieceVisual.transform.parent = transform;
        puzzlePieceVisual.transform.localPosition = Vector2.zero;

        _currentPuzzlePieceVisual = puzzlePieceVisual;

        CheckFinishHint();

        _puzzleUIBoard.OnAnotherPuzzlePieceCellAttached();
    }

    public void Empty()
    {
        ePieceCellState = EPieceCellState.empty;
        _currentPuzzlePieceVisual = null;
    }

    public void Hint()
    {
        if(_currentPuzzlePieceVisual != null)
        {
            _currentPuzzlePieceVisual.BackToListPieceUnassembled();
        }   

        GetComponent<FlickerSprite>().On();
    }

    private void CheckFinishHint()
    {
        if(_currentPuzzlePieceVisual.piece.GetKeyRowColumn() == correctKey)
        {
            GetComponent<FlickerSprite>().Off();
            _currentPuzzlePieceVisual.FinishHint();
            UIGameManager.Instance.FinishHint();
        }
    }

    public void Highlight()
    {
        if(ePieceCellState == EPieceCellState.empty)
        {
            _bgColor.color = Color.green;
        }
        else if(ePieceCellState == EPieceCellState.attached) 
        {
            _bgColor.color = Color.red;
        }

        if(_fadeOutRoutine != null) StopCoroutine(_fadeOutRoutine);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color startColor = _bgColor.color;
        float a = 1;
        while(a > 0)
        {
            yield return new WaitForEndOfFrame();
            a -= Time.deltaTime * 2;
            _bgColor.color = startColor * a;
        }
    }

    public bool IsEmpty() => ePieceCellState == EPieceCellState.empty;
    public PuzzlePieceVisual GetPiece() => _currentPuzzlePieceVisual;
}
