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
    float time = 0.0f;

    public int cardCount = 0;
    public int stage = 0;
    public float closeSpeed = 1f;

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
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0.0f;
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

            if (cardCount == 0)
            {
                board.gameObject.SetActive(false);
                timeTxt.gameObject.SetActive(false);

                if (stage == 1 || stage == 2)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                }

                else if(stage == 3)                                      //  ���� �������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    clearPanel.SetActive(true);
                    Time.timeScale = 0.0f;
                }

                Delete();
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

    public void Delete()                                                //  �������� ���� ��, ������ �������� ������ �ʱ�ȭ �� ���� ���������� �����͸� ���� �޾ƿ��� ���� ���� �Լ�
    {
        Debug.Log("���� �������� ���� ����");
        cardCount = 0;
        stage = 0;
        Time.timeScale = 1f;
        time = 0.0f;
        timeTxt.text = time.ToString("N2");
        //board.Delete();
    }
}
