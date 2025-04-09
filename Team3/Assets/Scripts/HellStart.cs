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
        GameManager.instance.level = 1;
        
        if (GameManager.instance.stage != 0 && GameManager.instance.level != 0)
        {
            Debug.Log("dd");
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 30f; // Ÿ�̸� �ʱ�ȭ
            GameManager.instance.isGamePlaying = true; // ���� ���ۻ���

            AudioManager.instance.ResetSpeed(); //��ġ ���� ����*
            AudioManager.instance.PlayMusic(); // BGM �̾ ���
        }
    }
}
