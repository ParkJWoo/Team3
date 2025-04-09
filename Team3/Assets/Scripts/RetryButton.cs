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

        GameManager.instance.time = 10.0f; // 타이머 초기화
        Time.timeScale = 1.0f;

        GameManager.instance.score = 0;


        if (GameManager.instance.hardPanel == true)
        {
            GameManager.instance.hardPanel.SetActive(false);
        }

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);

        if (GameManager.instance.failPanel == true)
        {
            GameManager.instance.failPanel.SetActive(false);
        }

        GameManager.instance.board.Start();
        GameManager.instance.isGamePlaying = true;
    }
}
