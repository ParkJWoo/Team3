using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card firstCard;
    public Card secondCard;

    AudioSource audioSource;
    public AudioClip clip;

    public GameObject clearPanel;                   //  ���� ���� Ŭ���� �� ������ ���� ���� �ǳ�
    public GameObject nextStagePanel;               //  �������� Ŭ���� �� ������ ���� �������� ���� �ǳ�
    public GameObject failPanel;

    public Text timeTxt;
    float time = 5.0f;

    public int cardCount = 16;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 0.0f)
        {
            Time.timeScale = 0.0f;
            failPanel.SetActive(true);                            //  ���� �ð� �� ī�带 ��� ������ ���� ���, ���� �ǳ� ���� �� �ð� ����
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;

            if(cardCount == 0)
            {
                nextStagePanel.SetActive(true);                       //  ī�带 ��� ������ ��, ���� ���������� �ƴ� ��, ���� �������� �ǳ� ����
                //clearPanel.SetActive(true);                         //  ī�带 ��� ������ ��, ���� ���������� ��, Ŭ���� �ǳ� ����
                Time.timeScale = 0.0f;
            }
        }

        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
