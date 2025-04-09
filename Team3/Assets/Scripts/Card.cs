using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

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
        if (GameManager.instance.secondCard != null) return;

        anim.SetBool("isClick", true);
        front.SetActive(true);
        back.SetActive(false);

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }

        if (GameManager.instance.firstCard == null) // ù��° ī������ �ι�° ī������ ����
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched(); // �ι�° ī�带 �����ϸ� Matched()�� ����
        }
    }

    public void DestroyCard()
    {
        anim.SetBool("isMatch",true);
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
        front.SetActive(false);
        back.SetActive(true);
        anim.SetBool("isClick", false);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
