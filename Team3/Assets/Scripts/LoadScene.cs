using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Animator anim;
   public void LoadMainScene()                                  //  MainScene »£√‚
    {
        //anim.SetBool("isClick", true);
        SceneManager.LoadScene("MainScene");
    }
}