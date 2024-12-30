using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : Singleton<Player>
{
    [SerializeField] private LevelConfig levelConfig;

    private PlayerData playerData;
    public PlayerData GetPlayerData => playerData;

    private void Awake() 
    {
        var playerData = JsonDataHandler.LoadData<PlayerData>();
        if(playerData == default)
        {
            InitNewDataPlayer();
        }
        else 
        {
            this.playerData = playerData;
        }
    }

    private void InitNewDataPlayer()
    {
        playerData = new PlayerData();
        playerData.currentLevel = 1;

        foreach(var level in levelConfig.Levels)
        {
            playerData.playerLevelDatas.Add(new PlayerLevelData(){
                isCurrentLevel = playerData.currentLevel == level.level,
                pass = false,
                level = level.level,
                star = 0,
                shortestTimeInSecond = float.MaxValue,
            });
        }

        JsonDataHandler.SaveData(playerData);
    }

    public void SavePlayerPassLevel(float time, int star)
    {
        var currentLevel = LevelSelection.Instance._currentLevelSelect;

        int i = 0;
        foreach(var pld in playerData.playerLevelDatas)
        {
            if(pld.level == currentLevel.level)
            {
                pld.pass = true;

                if(star > pld.star)
                {
                    pld.star = star;
                }

                if(time < pld.shortestTimeInSecond)
                {
                    pld.shortestTimeInSecond = time;
                }

                if(pld.isCurrentLevel)
                {
                    playerData.playerLevelDatas[i+1].isCurrentLevel = true;
                    pld.isCurrentLevel = false;
                }
            }

            i ++;
        }

        playerData = JsonDataHandler.SaveAndLoadData(playerData);
    }
}

[System.Serializable]
public class PlayerData {
    public int currentLevel = 0;
    public List<PlayerLevelData> playerLevelDatas = new List<PlayerLevelData>();
}

[System.Serializable]
public class PlayerLevelData {
    public string name;
    public string age;
    public int avatarName;
    public bool isCurrentLevel;
    public bool pass;
    public int level;
    public int star;
    public float shortestTimeInSecond;
}
