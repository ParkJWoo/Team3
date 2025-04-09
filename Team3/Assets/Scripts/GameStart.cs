using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public Animator anim;
    public GameObject levelPanel;

    //public float localTime { get; private set; } = 60.0f;

    public void LetGameStart()
    {
        AudioManager.instance.PlayClickSound(true);

        if (GameManager.instance.level == 1)
        {
            if (GameManager.instance.stage != 0)
            {
                anim.SetBool("isClick", true);
                levelPanel.SetActive(false);
                GameManager.instance.board.gameObject.SetActive(true);
                GameManager.instance.timeTxt.gameObject.SetActive(true);

                GameManager.instance.time = 60.0f;
                GameManager.instance.isGamePlaying = true;
                AudioManager.instance.ResetSpeed();
                AudioManager.instance.StopTickSfx();
                AudioManager.instance.PlayMusic();

            }
            
        }
        else if (GameManager.instance.level == 2)
        {
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.stage = 3;
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 10.0f;
            GameManager.instance.isGamePlaying = true;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.StopTickSfx();
            AudioManager.instance.PlayMusic();
        }
    }
}