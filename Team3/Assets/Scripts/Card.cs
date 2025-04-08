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

    public void Setting(int number) // number을 받아서 sprite를 가져 온다.
    {
        idx = number;
        frontImg.sprite = Resources.Load<Sprite>($"image{idx}");
    }

    public void OpenCard() // 카드를 오픈
    {
        if (GameManager.instance.secondCard != null) return;

        anim.SetBool("isClick", true);
        front.SetActive(true);
        back.SetActive(false);

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }

        if (GameManager.instance.firstCard == null) // 첫번째 카드인지 두번째 카드인지 구분
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched(); // 두번째 카드를 오픈하면 Matched()를 실행
        }
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

        Invoke("CloseCardInvoke", GameManager.instance.closeSpeed);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isClick", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
