using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public Animator anim;
    //public int cardCount = 4;

    public void RetryStageBtn()
    {
        //anim.SetBool("isClick", true);

        Debug.Log("retrybutton");
        AudioManager.instance.PlayClickSound();

        GameManager.instance.time = 60.0f; // Ÿ�̸� �ʱ�ȭ
        Time.timeScale = 1.0f;

        GameManager.instance.failPanel.SetActive(false);

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);

        if (GameManager.instance.failPanel == true)
        {
            Debug.Log("���� �ǳ� ����");
            GameManager.instance.failPanel.SetActive(false);
        }

        GameManager.instance.board.Start();
        GameManager.instance.isGamePlaying = true;
    }
}
