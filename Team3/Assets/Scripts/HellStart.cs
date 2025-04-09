using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;
    public float closeSpeed;
    public int stage;
    public int cardCount;
    

    public void LetHellStart()
    {
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;
        GameManager.instance.closeSpeed = closeSpeed;
        
        if (GameManager.instance.stage != 0 && GameManager.instance.closeSpeed != 0)
        {
            Debug.Log("BGM2");
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 30f;
            GameManager.instance.isGamePlaying = true;

            AudioManager.instance.ResetSpeed();
            AudioManager.instance.SwitchMusic(AudioManager.instance.hellClip,true);
        }
    }
}
