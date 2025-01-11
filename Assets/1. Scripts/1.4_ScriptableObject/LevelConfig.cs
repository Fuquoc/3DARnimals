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
    public bool canPlay;
    public int level;
    public string levelName;
    public int sizeMatrix;
    public Texture2D originText;
    public Animal animal;
    public LevelStarConfig levelStarConfig;
    public LevelSound levelSound;
    public LevelAnimalImageGallery levelAnimalImageGallery;
}

[System.Serializable]
public class LevelSound
{
    public AudioClip eyeAudioClip;
    public AudioClip noseAudioClip;
    public AudioClip mouthAudioClip;
    public AudioClip earAudioClip;
    public AudioClip brainAudioClip;
    public AudioClip footAudioClip;
    public AudioClip leatherAudioClip;
    public AudioClip tailAudioClip;
}

[System.Serializable]
public class LevelStarConfig
{
    public int maxSecondToTake3Star;
    public int maxSecondToTake2Star;
    public int maxSecondToTake1Star;
}

[System.Serializable]
public class LevelAnimalImageGallery
{
    public List<AnimalImageItem> animalImageItems;
    public string urlVideo;
}

[System.Serializable]
public class AnimalImageItem
{
    public string text;
    public Sprite image;
}
