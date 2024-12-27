using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [ContextMenu("Show Scale")]
    public void ShowLossyScale()
    {
        Debug.Log(transform.lossyScale);
    }
}
