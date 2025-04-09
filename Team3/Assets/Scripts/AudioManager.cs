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

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //BGM ������ҽ� �ʱ�ȭ
            if (SFXSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            //tick ȿ�����ҽ�
            GameObject sfxObject = new GameObject("TickSFXSource");
            sfxObject.transform.parent = this.transform;
            SFXSource = sfxObject.AddComponent<AudioSource>();
            SFXSource.playOnAwake = false;
            SFXSource.loop = true;
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
    }

    public void SetSpeed(float speed)
    {
        if (audioSource != null)
        {
            audioSource.pitch = speed;
        }
    }

    public void ResetSpeed()
    {//�����ӵ��� �ǵ�����
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
        //if (SFXSource == null)
        //{Debug.LogWarning("[AudioManager] sfxSource �ʱ�ȭ �ȵ�"); return;}

        if (!SFXSource.isPlaying)
        {
            SFXSource.clip = tickSfx;
            SFXSource.loop = true;
            SFXSource.pitch = 1.0f;
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
