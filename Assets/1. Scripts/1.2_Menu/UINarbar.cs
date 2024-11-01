using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINarbar : MonoBehaviour
{
    [SerializeField] private List<GameObject> listScreen;

    [SerializeField] private List<UIButtonNarbar> uIButtonNarbars;

    private void Awake() 
    {
        int i = 0;
        foreach(var btn in uIButtonNarbars)
        {
            btn.Attach(this, i);
            i ++;
        }
    }

    private void Start() 
    {
        ShowScreenByIndex(0);    
    }

    public void ShowScreenByIndex(int index)
    {
        for(int i = 0; i < listScreen.Count; i++)
        {
            if(i == index)
            {
                listScreen[i].gameObject.SetActive(true);
                uIButtonNarbars[i].Select();
            }
            else
            {
                listScreen[i].gameObject.SetActive(false);
                uIButtonNarbars[i].UnSelect();
            }
        }
    }
}
