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

    private EPieceCellState ePieceCellState = EPieceCellState.empty;
    private Coroutine _fadeOutRoutine;

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

    public void AttachThePiece(PuzzlePieceVisual puzzlePieceVisual)
    {
        puzzlePieceVisual.puzzlePieceCellAttach?.Empty();

        ePieceCellState = EPieceCellState.attached;
        puzzlePieceVisual.puzzlePieceCellAttach = this;
        puzzlePieceVisual.transform.parent = transform;
        puzzlePieceVisual.transform.localPosition = Vector2.zero;
    }

    public void Empty()
    {
        ePieceCellState = EPieceCellState.empty;
    }

    public bool IsEmpty() => ePieceCellState == EPieceCellState.empty;
}
