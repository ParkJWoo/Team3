using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip hellClip;

    public AudioSource SFXSource;
    public AudioClip tickSfx;

    public bool hasStarted = false;
    public bool isHell = false;

    public AudioSource clickSource;
    public AudioClip defaultClick;
    public AudioClip startClick;

    [Range(0f, 1f)] public float bgmVolume = 0.3f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float clickVolume = 0.4f;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //BGM 오디오소스 초기화
            if (SFXSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            //tick 효과음소스
            GameObject sfxObject = new GameObject("TickSFXSource");
            sfxObject.transform.parent = this.transform;
            SFXSource = sfxObject.AddComponent<AudioSource>();
            SFXSource.playOnAwake = false;
            SFXSource.loop = true;

            //click 효과음소스
            GameObject clickObject = new GameObject("ClickSFXSource");
            clickObject.transform.parent = this.transform;
            clickSource = clickObject.AddComponent<AudioSource>();
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
            audioSource.clip = normalClip;
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                hasStarted = true;
            }
        }
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = bgmVolume;
        }

        if (SFXSource != null)
        {
            SFXSource.volume = sfxVolume;
        }

        if (clickSource != null)
        {
            clickSource.volume = clickVolume;
        }
    }


    public void PlayClickSound(bool isStart = false)
    {
        if (clickSource == null) return;

        AudioClip clipToPlay = isStart ? startClick : defaultClick;
        clickSource.volume = clickVolume;
        clickSource.pitch = 1.0f;
        clickSource.loop = false;
        clickSource.PlayOneShot(clipToPlay);
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
        audioSource.volume = bgmVolume;
    }

    public void PlayTickSfx()
    {
        //if (SFXSource == null)
        //{Debug.LogWarning("[AudioManager] sfxSource 초기화 안됨"); return;}

        if (!SFXSource.isPlaying)
        {
            SFXSource.clip = tickSfx;
            SFXSource.loop = true;
            SFXSource.pitch = 1.0f;
            SFXSource.volume = sfxVolume;
            SFXSource.Play();
        }
    }

    public void StopTickSfx()
    {
        if (SFXSource != null && SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }
    }

    public bool IsHellMode()
    {
        return isHell;
    }
}