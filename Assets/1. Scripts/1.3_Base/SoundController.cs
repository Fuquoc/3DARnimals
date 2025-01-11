using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private List<SoundFX> listSoundFX;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundEffectSource;

    [Range(0, 1)] public float musicVolume = 1f; // Âm lượng nhạc nền
    [Range(0, 1)] public float soundEffectVolume = 1f; // Âm lượng sound effect

    private void Start() 
    {
        var cur = Player.Instance.GetPlayerData;

        _musicSource.volume = cur.musicVolumeSetting;
        _soundEffectSource.volume = cur.soundVolumeSetting;    
    }

    private void OnEnable()
    {
        SceneLoader.Instance.OnLoadScene += OnLoadScene;
    }

    private void OnDisable()
    {
        SceneLoader.Instance.OnLoadScene -= OnLoadScene;
    }

    protected override void Awake() 
    {
        base.Awake();
        
        // Tạo AudioSource cho nhạc nền nếu chưa có
        _musicSource.loop = true; // Nhạc nền sẽ lặp lại
        _musicSource.playOnAwake = false; // Không phát tự động
        _musicSource.volume = musicVolume;

        // Tạo AudioSource cho sound effect nếu chưa có
        _soundEffectSource.loop = false; // Sound effect không lặp
        _soundEffectSource.playOnAwake = false; // Không phát tự động
        _soundEffectSource.volume = soundEffectVolume;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SoundController: Clip nhạc nền không tồn tại!");
            return;
        }

        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }

        _musicSource.clip = clip;
        _musicSource.volume = musicVolume;
        _musicSource.Play();
    }

    public void PlaySoundEffectOneShot(SoundFXType soundFXType)
    {
        // Tìm clip phù hợp từ danh sách SoundFX
        SoundFX soundFX = listSoundFX.Find(sfx => sfx.soundFXType == soundFXType);

        if (soundFX == null || soundFX.audioClip == null)
        {
            Debug.LogWarning($"SoundController: Không tìm thấy sound effect cho {soundFXType}!");
            return;
        }

        _soundEffectSource.volume = soundEffectVolume;
        _soundEffectSource.PlayOneShot(soundFX.audioClip);

        // Bắt đầu điều chỉnh âm lượng nhạc nền
        StartCoroutine(AdjustMusicVolumeWhileSoundEffectPlays());
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SoundController: Clip sound effect không tồn tại!");
            return;
        }

        _soundEffectSource.volume = soundEffectVolume;
        _soundEffectSource.clip = clip;
        _soundEffectSource.Play();

        // Bắt đầu điều chỉnh âm lượng nhạc nền
        StartCoroutine(AdjustMusicVolumeWhileSoundEffectPlays());
    }


    public void ResumeSoundEffect()
    {
        if (_soundEffectSource.clip != null && !_soundEffectSource.isPlaying)
        {
            _soundEffectSource.Play();
        }
    }

    public void PauseSoundEffect()
    {
        if (_soundEffectSource.isPlaying)
        {
            _soundEffectSource.Pause();
        }
    }

    public void StopSoundEffect()
    {
        if (_soundEffectSource.isPlaying)
        {
            _soundEffectSource.Stop();
        }  
    } 

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        _musicSource.volume = musicVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
        _soundEffectSource.volume = soundEffectVolume;
    }

    public float GetMusicVolume()
    {
        return _musicSource.volume;
    }

    public float GetSoundVolume()
    {
        return _soundEffectSource.volume;
    }

    public void StopMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
    }

    private IEnumerator AdjustMusicVolumeWhileSoundEffectPlays()
    {
        // Giảm âm lượng nhạc nền
        _musicSource.volume = 0.1f;

        // Chờ sound effect phát xong
        while (_soundEffectSource.isPlaying)
        {
            yield return null;
        }

        // Phục hồi âm lượng nhạc nền
        _musicSource.volume = musicVolume;
    }

    private void OnLoadScene(int sceneIndex)
    {
        StopSoundEffect();
    }
}

[System.Serializable]
public class SoundFX
{
    public AudioClip audioClip;
    public SoundFXType soundFXType;
}

public enum SoundFXType
{
    Button,
    JigsawPut,
    WinGame
}