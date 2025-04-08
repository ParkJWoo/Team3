using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
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
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.PlayMusic(); // BGM 시작
        }
    }
}
