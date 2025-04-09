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

    public void LevelBtn()
    {
        GameManager.instance.level = level;
        GameManager.instance.stage = 0;                               //������������ �ʱ�ȭ
        GameManager.instance.cardCount = 0;
        if (level == 1)
        {
            easyanim.SetBool("isClick", true);
            hardanim.SetBool("isClick", false);

            anim1.SetBool("isClick", false);               //�������� ��ư �����ִ� �ʱ�ȭ
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);
        }
        else if (level == 2)
        {
            if (GameManager.instance.Clear >= 3)       //���� 3���ױ��� Ŭ�����ߴٸ�
            {
                hardanim.SetBool("isClick", true);
                easyanim.SetBool("isClick", false);

                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", false);
            }
            else                                      //���� 3���ױ��� Ŭ��� ���ߴٸ�
            {
                GameManager.instance.level = 1;
            }

        }
    }
}
