using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public float time = 60.0f;

    public bool isGamePlaying = false; //���� ���� ���� �Ǵ�

    public int cardCount = 16;

    // �������� Ŭ���� ���� �Ǵ�.
    public int Clear = 0;

    public int stage = 0;
    public float closeSpeed = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey("Clear"))
        {
            Clear = PlayerPrefs.GetInt("Clear");
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
        if (!isGamePlaying)
        {
            return;
        }

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        //if (time >= 30.0f)
        //{
        //    failPanel.SetActive(true);
        //    Time.timeScale = 0.0f;
        //}

        if (time <= 20.0f && AudioManager.instance.audioSource.pitch == 1.0f)
        {//���� �ð��� 20�� ������ ��, ����� �ӵ� ����
            AudioManager.instance.SetSpeed(1.5f);
        }

        if (time <= 0.0f)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0.0f;
            AudioManager.instance.ResetSpeed(); //���󺹱�
        }

        if (cardCount <= 0)
        {//���� ���� ���� Ȯ��
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed(); //���󺹱�
            timeTxt.text = time.ToString("N2");
            return;
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

                if(stage == 1 )                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    Clear = 1;
                    PlayerPrefs.SetInt("Clear", Clear);
                }

                if (stage == 2)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    Clear = 2;
                    PlayerPrefs.SetInt("Clear", Clear);
                }

                else if(stage == 3)                                      //  ���� �������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    clearPanel.SetActive(true);
                    Time.timeScale = 0.0f;
                    Clear = 3;
                    PlayerPrefs.SetInt("Clear", Clear);
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
        //stage = 0;
        Time.timeScale = 1f;
        time = 0.0f;
        timeTxt.text = time.ToString("N2");
        //board.Delete();
    }
}
