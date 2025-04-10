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
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();

        GameManager.instance.time = 60.0f; // Ÿ�̸� �ʱ�ȭ
        Time.timeScale = 1.0f;

        GameManager.instance.failPanel.SetActive(false);

        GameManager.instance.infinityPanel.SetActive(false);

        GameManager.instance.score = 0;

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);

        if (GameManager.instance.failPanel == true)
        {
            Debug.Log("���� �ǳ� ����");
            GameManager.instance.failPanel.SetActive(false);
        }

        if (GameManager.instance.optionPanel == true)
        {
            GameManager.instance.board.OnDisable();
            GameManager.instance.optionPanel.SetActive(false);
        }

        GameManager.instance.board.Start();
        GameManager.instance.isGamePlaying = true;

        
    }
}
