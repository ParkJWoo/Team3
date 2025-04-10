using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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

    public bool shouldTurnOffGhost = false;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.isGhost = false;
    }

    public void Setting(int number) // number을 받아서 sprite를 가져 온다.
    {
        idx = number;
        frontImg.sprite = Resources.Load<Sprite>($"image{idx}");
    }

    public void OpenCard() // 카드를 오픈
    {
        if (GameManager.instance.secondCard != null) return;

        // isGhost가 true인 상태에서는 카드를 오픈할 수 없도록 리턴
        if (GameManager.instance.isGhost == true) return;

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
        DestroyCardInvoke();
    }

    public void DestroyCard(float time)
    {
        anim.SetBool("isMatch", true);
        Invoke("DestroyCardInvoke", time);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);

        //첫번째 카드가 파괴된 후에만 isGhost를 false로 바꿔준다.
        //Ghost의 Ani가 끝난 후(카드가 파괴된 3초 후)에 게임 진행 가능.
        if (shouldTurnOffGhost && GameManager.instance != null)
        {
            GameManager.instance.isGhost = false;
        }
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
