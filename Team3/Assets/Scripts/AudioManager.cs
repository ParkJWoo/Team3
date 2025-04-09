using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip normalClip;
    public AudioClip hellClip;

    public bool hasStarted = false;
    public bool isHell = false;

    public void Awake() //싱글톤 패턴
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
                audioSource.clip = normalClip;
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

        Debug.Log("BGM 전환");
    }

    public bool IsHellMode()
    {
        return isHell;
    }
}
