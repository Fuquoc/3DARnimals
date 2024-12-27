using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Thêm AudioSource nếu chưa có
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false; // Sound effect thường không lặp
        audioSource.playOnAwake = false; // Không phát tự động
    }

    /// <summary>
    /// Phát sound effect.
    /// Nếu có âm thanh khác đang phát, nó sẽ bị dừng và thay thế bằng âm thanh mới.
    /// </summary>
    /// <param name="clip">Âm thanh cần phát</param>
    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("SoundEffectManager: Clip không tồn tại!");
            return;
        }

        // Nếu đang phát âm thanh khác, dừng nó trước
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Phát âm thanh mới
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Phát tiếp tục âm thanh hiện tại nếu có.
    /// </summary>
    public void ResumeSoundEffect()
    {
        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Dừng phát sound effect hiện tại.
    /// </summary>
    public void PauseSoundEffect()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
