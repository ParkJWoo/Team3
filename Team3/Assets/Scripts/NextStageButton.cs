using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageButton : MonoBehaviour
{
    public Animator anim;
    public int cardCount = 4;

    public void NextStageBtn()
    {
        //anim.SetBool("isClick", true);

        //Debug.Log(GameManager.instance.stage);
        //Debug.Log(GameManager.instance.cardCount);

        GameManager.instance.nextPanel.SetActive(false);

        //GameManager.instance.cardCount = 4;

        //GameManager.instance.stage += 1;
        //GameManager.instance.cardCount += 4;

        //cardCount += 4;

        GameManager.instance.stage += 1;
        GameManager.instance.cardCount = cardCount * 2;

        //stage = GameManager.instance.stage + 1;
        //cardCount = GameManager.instance.cardCount * 2;

        Debug.Log(GameManager.instance.stage);
        Debug.Log(GameManager.instance.cardCount);

        GameManager.instance.board.Start();
        GameManager.instance.isGamePlaying = true;

        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);
    }
}