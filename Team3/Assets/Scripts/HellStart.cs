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
        AudioManager.instance.PlayClickSound(true);

        GameManager.instance.stage = 4;
        GameManager.instance.cardCount = cardCount;
        GameManager.instance.level = 1;

        if (GameManager.instance.stage != 0 && GameManager.instance.level != 0)
        {
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 30f;
            GameManager.instance.isGamePlaying = true;

            AudioManager.instance.ResetSpeed();
        }
    }
}
