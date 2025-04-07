using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anime;

    public SpriteRenderer frontImg;
    AudioSource audioSource;
    public AudioClip clip;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number) // number�� �޾Ƽ� sprite�� ���� �´�.
    {
        idx = number;
        frontImg.sprite = Resources.Load<Sprite>($"image{idx}");
    }

    public void OpenCard() // ī�带 ����
    {

    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1f);
    }

    void CloseCardInvoke()
    {
        anime.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
