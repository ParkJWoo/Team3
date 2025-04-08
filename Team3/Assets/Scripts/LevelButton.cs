using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Animator easyanim;
    public Animator hardanim;
    public float closeSpeed;

    public void LevelBtn()
    { 
        GameManager.instance.closeSpeed = closeSpeed;

        if(closeSpeed == 1)
        {
            easyanim.SetBool("isClick", true);
            hardanim.SetBool("isClick", false);
        }
        else if(closeSpeed == 0.5)
        {
            hardanim.SetBool("isClick", true);
            easyanim.SetBool("isClick", false);
        }
    }
}
