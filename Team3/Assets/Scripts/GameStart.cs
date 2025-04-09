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

        if (GameManager.instance.stage != 0 && GameManager.instance.level != 0)
        {
            AudioManager.instance.PlayClickSound(true);
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 5.0f; // Ÿ�̸� �ʱ�ȭ
            GameManager.instance.isGamePlaying = true; // ���� ���ۻ���

            AudioManager.instance.ResetSpeed(); //��ġ ���� ����*
            AudioManager.instance.PlayMusic(); // BGM �̾ ���
        }
    }
}