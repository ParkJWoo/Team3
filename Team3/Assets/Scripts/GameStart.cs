using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;

    //public float localTime { get; private set; } = 60.0f;

    public void LetGameStart()
    {
        if(GameManager.instance.level == 1)                         //기본모드 누르고 시작버튼누를시
        {
            if (GameManager.instance.stage != 0)
            {
                anim.SetBool("isClick", true);
                levelPanel.SetActive(false);
                GameManager.instance.board.gameObject.SetActive(true);
                GameManager.instance.timeTxt.gameObject.SetActive(true);

                GameManager.instance.time = 60.0f; // 타이머 초기화
                GameManager.instance.isGamePlaying = true; // 게임 시작상태

                AudioManager.instance.ResetSpeed(); //피치 리셋 먼저*
                AudioManager.instance.PlayMusic(); // BGM 이어서 재생
            }
        }
        else if(GameManager.instance.level == 2)                     //무한모드 누르고 시작버튼누를시
        {
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.stage = 3;           //기본 3스테 카드보드를 따라감

            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 10.0f; // 타이머 초기화
            GameManager.instance.isGamePlaying = true; // 게임 시작상태

            AudioManager.instance.ResetSpeed(); //피치 리셋 먼저*
            AudioManager.instance.PlayMusic(); // BGM 이어서 재생
        }
    }
}