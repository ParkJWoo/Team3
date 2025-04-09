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
        if(GameManager.instance.level == 1)                         //�⺻��� ������ ���۹�ư������
        {
            if (GameManager.instance.stage != 0)
            {
                anim.SetBool("isClick", true);
                levelPanel.SetActive(false);
                GameManager.instance.board.gameObject.SetActive(true);
                GameManager.instance.timeTxt.gameObject.SetActive(true);

                GameManager.instance.time = 60.0f; // Ÿ�̸� �ʱ�ȭ
                GameManager.instance.isGamePlaying = true; // ���� ���ۻ���

                AudioManager.instance.ResetSpeed(); //��ġ ���� ����*
                AudioManager.instance.PlayMusic(); // BGM �̾ ���
            }
        }
        else if(GameManager.instance.level == 2)                     //���Ѹ�� ������ ���۹�ư������
        {
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.stage = 3;           //�⺻ 3���� ī�庸�带 ����

            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 10.0f; // Ÿ�̸� �ʱ�ȭ
            GameManager.instance.isGamePlaying = true; // ���� ���ۻ���

            AudioManager.instance.ResetSpeed(); //��ġ ���� ����*
            AudioManager.instance.PlayMusic(); // BGM �̾ ���
        }
    }
}