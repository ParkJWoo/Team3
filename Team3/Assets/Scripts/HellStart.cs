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
    public float localTime { get; private set; } = 0.0f;

    public void LetHellStart()
    {
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;
        GameManager.instance.closeSpeed = closeSpeed;
        
        if (GameManager.instance.stage != 0 && GameManager.instance.closeSpeed != 0)
        {
            Debug.Log("dd");
            
            anim.SetBool("isClick", true);
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);

            GameManager.instance.time = 0f; // Ÿ�̸� �ʱ�ȭ
            GameManager.instance.isGamePlaying = true; // ���� ���ۻ���

            AudioManager.instance.ResetSpeed(); //��ġ ���� ����*
            AudioManager.instance.PlayMusic(); // BGM �̾ ���
        }
    }
}
