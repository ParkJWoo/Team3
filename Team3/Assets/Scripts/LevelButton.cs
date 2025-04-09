using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Animator easyanim;
    public Animator hardanim;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public int level;

    public GameObject st1Btn;
    public GameObject st2Btn;
    public GameObject st3Btn;

    public GameObject startBtn;

    public void LevelBtn()
    {
        GameManager.instance.level = level;
        GameManager.instance.stage = 0;                               //������������ �ʱ�ȭ
        GameManager.instance.cardCount = 0;
        if (level == 1)                                     //�⺻��� Ŭ����
        {
            anim1.SetBool("isClick", false);               //�������� ��ư �����ִ� �ʱ�ȭ
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);
            
            st1Btn.SetActive(true);                       
            st2Btn.SetActive(true);
            st3Btn.SetActive(true);
            
            startBtn.SetActive(false);

            easyanim.SetBool("isClick", true);
            hardanim.SetBool("isClick", false);
            
        }
        else if (level == 2)
        {
            anim1.SetBool("isClick", false);
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);

            st1Btn.SetActive(false);
            st2Btn.SetActive(false);
            st3Btn.SetActive(false);

            startBtn.SetActive(false);

            easyanim.SetBool("isClick", false);
            if (GameManager.instance.Clear >= 3)       //�⺻ 3���ױ��� Ŭ�����ߴٸ�
            {
                hardanim.SetBool("isClick", true);
                startBtn.SetActive(true);
            }
            else                                      //�⺻ 3���ױ��� Ŭ��� ���ߴٸ�
            {
                GameManager.instance.level = 1;
            }

        }
    }
}
