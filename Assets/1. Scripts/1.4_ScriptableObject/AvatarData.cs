using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvatarData", menuName = "ScriptableObjects/AvatarData", order = 1)]
public class AvatarData : ScriptableObject
{
    public List<AvatarStruct> avatarStructs = new List<AvatarStruct>();    
}


[System.Serializable]
public class AvatarStruct
{
    public string avatarName;
    public Sprite avatarIcon;
}
