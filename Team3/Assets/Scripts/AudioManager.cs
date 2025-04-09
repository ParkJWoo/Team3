using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip hellClip;

    public AudioSource sfxSource;
    public AudioClip tickSfx;

    public bool hasStarted = false;
    public bool isHell = false;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (!hasStarted)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = normalClip;
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                hasStarted = true;
            }
        }

        if (sfxSource == null)
        {
            GameObject sfxObject = new GameObject("SFXSource");
            sfxObject.transform.parent = this.transform;
            sfxSource = sfxObject.AddComponent<AudioSource>();
            sfxSource.loop = true;
        }
    }

    public void SetSpeed(float speed)
    {
        if (audioSource != null)
        {
            audioSource.pitch = speed;
        }
    }

    public void ResetSpeed()
    {//원래속도로 되돌리기
        SetSpeed(1.0f);
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void SwitchMusic(bool toHellMode)
    {
        if (audioSource == null) return;

        AudioClip targetClip = toHellMode ? hellClip : normalClip;

        if (audioSource.clip == targetClip)
        {
            return;
        }

        isHell = toHellMode;
        audioSource.Stop();
        audioSource.clip = targetClip;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }

    public void PlayTickSfx()
    {
        if (sfxSource != null && !sfxSource.isPlaying)
        {
            sfxSource.clip = tickSfx;
            sfxSource.pitch = 1.0f; // pitch 공유 X
            sfxSource.Play();

            Debug.Log("Play tick sfx: " + sfxSource.clip.name);
        }
    }

    public void StopTickSfx()
    {
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();

            Debug.Log("[GameManager] 시간 20초 이하 - BGM 속도 증가 + 효과음 재생 시작");
        }
    }

    public bool IsHellMode()
    {
        return isHell;
    }
}
