using System.Collections;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    private AudioSource _musicSource;
    private AudioSource _soundEffectSource;

    [Range(0, 1)] public float musicVolume = 1f; // Âm lượng nhạc nền
    [Range(0, 1)] public float soundEffectVolume = 1f; // Âm lượng sound effect

    private void Awake()
    {
        // Tạo AudioSource cho nhạc nền nếu chưa có
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true; // Nhạc nền sẽ lặp lại
        _musicSource.playOnAwake = false; // Không phát tự động
        _musicSource.volume = musicVolume;

        // Tạo AudioSource cho sound effect nếu chưa có
        _soundEffectSource = gameObject.AddComponent<AudioSource>();
        _soundEffectSource.loop = false; // Sound effect không lặp
        _soundEffectSource.playOnAwake = false; // Không phát tự động
        _soundEffectSource.volume = soundEffectVolume;
    }

    /// <summary>
    /// Phát nhạc nền.
    /// </summary>
    /// <param name="clip">Âm thanh nhạc nền</param>
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

    /// <summary>
    /// Phát sound effect.
    /// </summary>
    /// <param name="clip">Âm thanh cần phát</param>
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

    /// <summary>
    /// Điều chỉnh âm lượng nhạc nền.
    /// </summary>
    /// <param name="volume">Giá trị âm lượng từ 0 đến 1</param>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        _musicSource.volume = musicVolume;
    }

    /// <summary>
    /// Điều chỉnh âm lượng sound effect.
    /// </summary>
    /// <param name="volume">Giá trị âm lượng từ 0 đến 1</param>
    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
        _soundEffectSource.volume = soundEffectVolume;
    }

    /// <summary>
    /// Dừng nhạc nền.
    /// </summary>
    public void StopMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
    }
}
