using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;

    public int stage;
    public int cardCount;

    public void StageBtn()
    {
        //�����̵� Ŭ�������� ����2��ư Ŭ�������� Ŭ��� 4�̸��̸� ��Ŭ��
        switch (stage)
        {
            case 1:
                if (GameManager.instance.closeSpeed == 0.5f)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 3)         //�����̵� ����3�� �����ٸ�
                    {
                        return;
                    }

                }
                anim1.SetBool("isClick", true);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", false);
                break;
            case 2:
                if (GameManager.instance.Clear < 1)
                {
                    return;
                }
                if(GameManager.instance.closeSpeed == 0.5f)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 4)         //������̵� ����1�� �����ٸ�
                    {
                        return;
                    }
                    
                }                                               //�� �����ǹ��� ����ϸ� ����
                    anim1.SetBool("isClick", false);
                    anim2.SetBool("isClick", true);
                    anim3.SetBool("isClick", false);
                    break;
            case 3:
                if (GameManager.instance.Clear < 2)
                {
                    return;
                }
                if (GameManager.instance.closeSpeed == 0.5f)     //������̵� ���ý�
                {
                    if (GameManager.instance.Clear < 5)         //������̵� ����2�� �����ٸ�
                    {
                        return;
                    }

                }                                               //�� �����ǹ��� ����ϸ� ����
                    anim1.SetBool("isClick", false);
                    anim2.SetBool("isClick", false);
                    anim3.SetBool("isClick", true);
                    break;
        }
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;

    }
}
