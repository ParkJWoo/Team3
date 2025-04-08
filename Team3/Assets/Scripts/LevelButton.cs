using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public Animator anim;
    public float closeSpeed;

    public void LevelBtn()
    {
        anim.SetBool("isClick", true);
        GameManager.instance.closeSpeed = closeSpeed;
    }
}
