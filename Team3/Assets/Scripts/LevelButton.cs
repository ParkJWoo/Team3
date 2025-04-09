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
    public float closeSpeed;

    public GameObject st1btn;
    public GameObject st2btn;
    public GameObject st3btn;
    public GameObject startbtn;


    public void LevelBtn()
    { 
        st1btn.SetActive(true);
        st2btn.SetActive(true);
        st3btn.SetActive(true);
        startbtn.SetActive(false);

        GameManager.instance.closeSpeed = closeSpeed;
        GameManager.instance.stage = 0;                               //스테이지변수 초기화
        GameManager.instance.cardCount = 0;
        if (closeSpeed == 1)                                               
        {
            easyanim.SetBool("isClick", true);
            hardanim.SetBool("isClick", false);

            anim1.SetBool("isClick", false);               //스테이지 버튼 눌린애니 초기화
            anim2.SetBool("isClick", false);
            anim3.SetBool("isClick", false);
        }
        else if(closeSpeed == 0.5)
        {
            if(GameManager.instance.Clear >= 3)       //쉬움 3스테까지 클리어했다면
            {
                hardanim.SetBool("isClick", true);
                easyanim.SetBool("isClick", false);
                
                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", false);
            }
            else                                      //쉬움 3스테까지 클리어를 못했다면
            {
                GameManager.instance.closeSpeed = 1;
            }
            
        }
    }
}
