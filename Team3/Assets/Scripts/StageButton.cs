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
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;
        switch (stage)
        {
            case 1:
                anim1.SetBool("isClick", true);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", false);
                break;
            case 2:
                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", true);
                anim3.SetBool("isClick", false);
                break;
            case 3:
                anim1.SetBool("isClick", false);
                anim2.SetBool("isClick", false);
                anim3.SetBool("isClick", true);
                break;

        }
    }
}
