using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;

    public GameObject startbtn;

    public int stage;
    public int cardCount;

    public void StageBtn()
    {
        switch (stage)
        {
            case 1:
                /*if (GameManager.instance.level == 2)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 3)         //�����̵� ����3�� �����ٸ�
                    {
                        return;
                    }

                }*/
                anim1.SetBool("isClick", true);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", false);
                break;
            case 2:
                if (GameManager.instance.Clear < 1)
                {
                    return;
                }
                /*if (GameManager.instance.level == 2)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 4)         //������̵� ����1�� �����ٸ�
                    {
                        return;
                    }

                } */                                              //�� �����ǹ��� ����ϸ� ����
                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", true);
                anim3.SetBool("isClick", false);
                break;
            case 3:
                if (GameManager.instance.Clear < 2)
                {
                    return;
                }
                /*
                if (GameManager.instance.level == 2)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 5)         //������̵� ����2�� �����ٸ�
                    {
                        return;
                    }

                } */                                              //�� �����ǹ��� ����ϸ� ����
                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", true);
                break;
        }
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;

        if (anim1.GetBool("isClick") || anim2.GetBool("isClick") || anim3.GetBool("isClick"))           //��ư�� �ϳ��� �������¸� ��ŸƮ��ư ����
        {
            startbtn.SetActive(true);
        }
        else
        {
            startbtn.SetActive(false);
        }

    }
}
