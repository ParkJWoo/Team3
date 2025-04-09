using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NextStageButton : MonoBehaviour
{
    public Animator anim;
    public int cardCount = 4;

    public void NextStageBtn()
    {
        //anim.SetBool("isClick", true);

        GameManager.instance.nextPanel.SetActive(false);

        if (GameManager.instance.level == 2)     //어려움난이도 선택시
        {
            if (GameManager.instance.Clear < 3)         //쉬움난이도 스테3을 못깼다면
            {
                return;
            }

        }

        GameManager.instance.time = 60.0f; // 타이머 초기화
        Time.timeScale = 1.0f;

        GameManager.instance.stage += 1;
        GameManager.instance.cardCount = cardCount * 2;


        GameManager.instance.isGamePlaying = true;

        Debug.Log(GameManager.instance.stage);
        Debug.Log(GameManager.instance.cardCount);

        GameManager.instance.board.Start();

        Debug.Log(GameManager.instance.time);

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);
    }
}