using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public Animator anim;
    public int stage;
    public int cardCount;
    public void StageBtn()
    {
        anim.SetBool("isClick", true);
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;
    }
}
