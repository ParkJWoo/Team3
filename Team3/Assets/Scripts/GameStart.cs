using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;

    public float localTime { get; private set; } = 0.0f;

    public void LetGameStart()
    {

        if (GameManager.instance.stage != 0 && GameManager.instance.closeSpeed != 0)
        {
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 0f; // 타이머 초기화
            GameManager.instance.isGamePlaying = true; // 게임 시작상태

            AudioManager.instance.ResetSpeed(); //피치 리셋 먼저*
            AudioManager.instance.PlayMusic(); // BGM 이어서 재생
        }
    }
}