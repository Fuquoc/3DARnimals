using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigData", menuName = "ScriptableObjects/LevelConfigData", order = 1)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfigData> levels;

    public List<LevelConfigData> Levels => levels;
}

[System.Serializable]
public class LevelConfigData
{
    public int level;
    public string levelName;
    public int sizeMatrix;
    public Texture2D originText;
    public Animal animal;
    public bool isLock;
}
