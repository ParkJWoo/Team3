using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card firstCard;
    public Card secondCard;
    public Board board;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip wrongClip;

    public GameObject clearPanel; // ���� �������� Ŭ���� �� ������ ���� ���� �ǳ�
    public GameObject nextPanel;  // ���� ���������� �ƴ� �������� Ŭ���� �� ������ ���� �������� �̵� �ǳ�
    public GameObject failPanel;  // ���� �ð��� ���� �� ������ ���� �ǳ�

    public Text timeTxt;
    public float time = 0.0f;

    // �������� Ŭ���� ���� �Ǵ�.
    public bool isClear_1 = false;
    public bool isClear_2 = false;
    public bool isClear_3 = false;

    public int cardCount = 16;
    public int stage = 0;
    public float closeSpeed = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        board.gameObject.SetActive(false);
        timeTxt.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        stage = 0;
        closeSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (stage != 0 && GameManager.instance.cardCount > 0 && time >= 20f && AudioManager.instance.audioSource.pitch == 1.0)
        {//20�� ������ bgm �ӵ� ����
            AudioManager.instance.SetSpeed(1.5f);
        }

        if (cardCount <= 0)
        {//���� �� ���� Ȯ��
            AudioManager.instance.ResetSpeed();
            timeTxt.text = time.ToString("N2");
            return;
        }

        //if(time >= 30.0f)
        //{
        //    failPanel.SetActive(true);
        //    Time.timeScale = 0.0f;
        //}
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;

            if (cardCount == 0)
            {
                if(stage == 1 )                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    isClear_1 = true;
                }

                if (stage == 2)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    isClear_2 = true;
                }

                else if(stage == 3)                                      //  ���� �������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    clearPanel.SetActive(true);
                    Time.timeScale = 0.0f;
                    isClear_3 = true;
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(wrongClip);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
