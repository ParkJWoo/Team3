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
        anim.SetBool("isClick", true);
        levelPanel.SetActive(false);

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);

        GameManager.instance.time = 0f; // 타이머 초기화
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.PlayMusic(); // BGM 시작
    }
}
