using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINewPlayer : MonoBehaviour
{
    [SerializeField] private EnterAge enterAge;
    [SerializeField] private EnterName enterName;
    [SerializeField] private EnterAvatar enterAvatar;
    [SerializeField] private GameObject bg;

    private PlayerData currentPlayerData;

    private void Start()
    {
        currentPlayerData = JsonDataHandler.LoadData<PlayerData>();

        if(currentPlayerData.avatarName == string.Empty)
        {
            enterName.gameObject.SetActive(true);
            bg.SetActive(true);
        }
    }

    public void EnterName(string name)
    {
        currentPlayerData.name = name;
        enterAge.gameObject.SetActive(true);
        enterName.gameObject.SetActive(false);
    }

    public void EnterAge(int age)
    {
        currentPlayerData.age = age;
        enterAge.gameObject.SetActive(false);
        enterAvatar.gameObject.SetActive(true);
    }

    public void EnterAvatar(string avtname)
    {
        currentPlayerData.avatarName = avtname;
        enterAvatar.gameObject.SetActive(false);

        JsonDataHandler.SaveData(currentPlayerData);
    }
}
