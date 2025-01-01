using System.Collections;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundEffectSource;

    [Range(0, 1)] public float musicVolume = 1f; // Âm lượng nhạc nền
    [Range(0, 1)] public float soundEffectVolume = 1f; // Âm lượng sound effect

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

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SoundController: Clip sound effect không tồn tại!");
            return;
        }

        _soundEffectSource.volume = soundEffectVolume;
        _soundEffectSource.PlayOneShot(clip); // PlayOneShot cho phép phát nhiều sound effect đồng thời
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
}
