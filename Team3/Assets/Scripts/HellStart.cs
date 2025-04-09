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
        GameManager.instance.stage = 4;
        GameManager.instance.cardCount = cardCount;
        GameManager.instance.closeSpeed = closeSpeed;
        
        if (GameManager.instance.stage != 0 && GameManager.instance.closeSpeed != 0)
        {
            Debug.Log("dd");
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 60f; // 타이머 초기화
            GameManager.instance.isGamePlaying = true; // 게임 시작상태

            AudioManager.instance.ResetSpeed(); //피치 리셋 먼저*
            AudioManager.instance.PlayMusic(); // BGM 이어서 재생
        }
    }
}
