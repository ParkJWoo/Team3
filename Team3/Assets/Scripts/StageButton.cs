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
        //쉬움난이도 클리어한후 스테2버튼 클릭했을때 클리어가 4미만이면 못클릭
        switch (stage)
        {
            case 1:
                if (GameManager.instance.closeSpeed == 0.5f)     //어려움난이도 선택시
                {
                    if (GameManager.instance.Clear < 3)         //쉬움난이도 스테3을 못깼다면
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
                if(GameManager.instance.closeSpeed == 0.5f)     //어려움난이도 선택시
                {
                    if (GameManager.instance.Clear < 4)         //어려움난이도 스테1을 못깼다면
                    {
                        return;
                    }
                    
                }                                               //위 두조건문을 통과하면 실행
                    anim1.SetBool("isClick", false);
                    anim2.SetBool("isClick", true);
                    anim3.SetBool("isClick", false);
                    break;
            case 3:
                if (GameManager.instance.Clear < 2)
                {
                    return;
                }
                if (GameManager.instance.closeSpeed == 0.5f)     //어려움난이도 선택시
                {
                    if (GameManager.instance.Clear < 5)         //어려움난이도 스테2을 못깼다면
                    {
                        return;
                    }

                }                                               //위 두조건문을 통과하면 실행
                    anim1.SetBool("isClick", false);
                    anim2.SetBool("isClick", false);
                    anim3.SetBool("isClick", true);
                    break;
        }
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;

    }
}
