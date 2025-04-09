using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UIImg = UnityEngine.UI.Image;

using UnityEngineInternal;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;
    public Card etcCard;
    public Board board;


    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip wrongClip;

    public GameObject clearPanel; // ���� �������� Ŭ���� �� ������ ���� ���� �ǳ�
    public GameObject nextPanel;  // ���� ���������� �ƴ� �������� Ŭ���� �� ������ ���� �������� �̵� �ǳ�
    public GameObject failPanel;  // ���� �ð��� ���� �� ������ ���� �ǳ�
    public GameObject hiddenPanel; // ���� �������� Ŭ���� �� ������ �ǳ�

    public GameObject hiddenBtn;
    public GameObject hardBtn;
    public UIImg st2Btnimg;
    public UIImg st3Btnimg;

    public Text timeTxt;
    public float time = 60.0f;

    public bool isGamePlaying = false; //���� ���� ���� �Ǵ�

    public int cardCount = 16;

    // �������� Ŭ���� ���� �Ǵ�.
    public int Clear = 0;

    public int stage = 0;
    public int level = 0;
    public int score = 0;
    public int bestScore = 0;

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

        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
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
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text hardbtnText = hardBtn.GetComponentInChildren<Text>();
        if (Clear >= 3)
        {
            hardbtnText.text = "���� ���";
        }
        else
        {
            hardbtnText.text = "���� ���(���)";
        }

        if (level == 1)
        {
            if (Clear < 3)                                      //�����带 ��� Ŭ���������ʾ�����
            {
                st2Btnimg.enabled = true;                       //���ǥ�� ���̰� �ʱ�ȭ
                st3Btnimg.enabled = true;
            }
            else
            {
                st2Btnimg.enabled = false;                       //��� Ŭ���������� �����̹��� ����
                st3Btnimg.enabled = false;
            }
                switch (Clear)
                {
                    case 0:
                        break;
                    case 1:
                        st2Btnimg.enabled = false;
                        break;
                    case 2:
                        st2Btnimg.enabled = false;
                        st3Btnimg.enabled = false;
                        break;
                    case 3:
                        st2Btnimg.enabled = false;
                        st3Btnimg.enabled = false;
                        break;

                }
        }
        /*else if (level == 2)
        {
            if (Clear < 6)                                      //������带 ��� Ŭ���������ʾ�����
            {
                st2Btnimg.enabled = true;                       //���ǥ�� �ʱ�ȭ
                st3Btnimg.enabled = true;
            }
            switch (Clear)
            {
                case 4:
                    st2Btnimg.enabled = false;
                    break;
                case 5:
                    st2Btnimg.enabled = false;
                    st3Btnimg.enabled = false;
                    break;

            }
        }*/


        if (score >= 20f)                        //�����ư ���Ѹ�� ����10�̻�� ����
        {
            hiddenBtn.SetActive(true);
        }
        else
        {
            hiddenBtn.SetActive(false);
        }

        //ī����� ���۽�
        if (!isGamePlaying)
        {
            return;
        }

        if (stage == 4 && !AudioManager.instance.IsHellMode())
        {
            AudioManager.instance.SwitchMusic(true);
        }

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        //if (time >= 30.0f)
        //{
        //    failPanel.SetActive(true);
        //    Time.timeScale = 0.0f;
        //}

        if (time <= 20.0f && AudioManager.instance.audioSource.pitch == 1.0f)
        {
            AudioManager.instance.SetSpeed(1.5f);
            AudioManager.instance.PlayTickSfx();
        }

        if (level == 1 && time <= 0.0f && cardCount != 0)
        {
            failPanel.SetActive(true);
            Time.timeScale = 1.0f;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.SwitchMusic(false);
            AudioManager.instance.StopTickSfx();
            isGamePlaying = false;
            board.gameObject.SetActive(false);
        }

        if (cardCount <= 0 && level == 1)
        {//���� ���� ���� Ȯ��
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed(); //���󺹱�
            AudioManager.instance.ResetSpeed();

            if (stage == 4)
            {
                AudioManager.instance.SwitchMusic(false);
            }

            board.gameObject.SetActive(false);

        }

        if ((cardCount == 0 && level == 1) || (time <= 0 && level == 2))
        {
            timeTxt.gameObject.SetActive(false);

            GameClear(stage,score);

            Delete();
        }
    }

    public void Matched()
    {

        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            if (secondCard.idx == 8)
            {
                firstCard.DestroyCard(0f);
                secondCard.front.GetComponent<Animator>().SetBool("isOpen", true);
                secondCard.transform.position = new Vector2(0, 0);
                secondCard.DestroyCard(3f);
            }
            else
            {
                firstCard.DestroyCard(1f);
                secondCard.DestroyCard(1f);
            }

            cardCount -= 2;

            
            if (level == 2)
            {
                score++;
                if (cardCount == 0)
                {
                    Invoke("ReBoard", 1f);
                    Debug.Log("score: " + score);
                    
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

    public void GameClear(int _stage,int _score) // ������ ������ ���
    {
        //nextPanel.SetActive(true); //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����

        int pastBestScore = PlayerPrefs.GetInt("bestScore", bestScore); // ���Ѹ�� �ְ�����
        int pastStage = PlayerPrefs.GetInt("Clear", Clear); // ���� �ְ� ��������
        Time.timeScale = 0.0f;

        if(stage >= 0 && stage <= 2)
        {
            nextPanel.SetActive(true);
        }

        else if(stage == 3)
        {
            clearPanel.SetActive(true);
        }

        else if (stage == 4)
        {
            hiddenPanel.SetActive(true);
        }

        if (level == 1)
        {
            Clear = _stage;

            //if (stage == 4)
            //{
            //    clearPanel.SetActive(true);
            //}
        }
        else if (level == 2)
        {
            Clear = _stage + 3;
            board.OnDisable();

            if (score >= pastBestScore)
            {
                PlayerPrefs.SetInt("bestScore", score);  
                PlayerPrefs.Save(); 
                Debug.Log("�ְ� ���� �����: " + score);
            }
        }

        if (pastStage < _stage)
        {
            PlayerPrefs.SetInt("Clear", Clear);
        }
    }

    public void Delete()                                                //  �������� ���� ��, ������ �������� ������ �ʱ�ȭ �� ���� ���������� �����͸� ���� �޾ƿ��� ���� ���� �Լ�
    {
       
        cardCount = 0;
        Time.timeScale = 1f;
        time = 0.0f;
        timeTxt.text = time.ToString("N2");
        if (level == 2)
        {
            score = 0;  // ���Ѹ�� ���� �ʱ�ȭ
        }
    }

    private void ReBoard()
    {
        board.Start();
    }
}
