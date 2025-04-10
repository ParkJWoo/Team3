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
    public GameObject infinityPanel;
    public GameObject optionPanel;
    public GameObject optionButton; //���� ��� �ɼǹ�ư

    public GameObject hiddenBtn;
    public GameObject hardBtn;
    public UIImg st2Btnimg;
    public UIImg st3Btnimg;

    public Text timeTxt;
    public float time = 60.0f;

    public Text nowScoreTxt;
    public Text bestScoreTxt;
    public Text cbestScoreTxt;

    public bool isGamePlaying = false; //���� ���� ���� �Ǵ�

    public int cardCount = 16;

    // �������� Ŭ���� ���� �Ǵ�.
    public int Clear = 0;

    public bool isGhost = false;

    public int stage = 0;
    public int mode = 0;
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
        optionButton.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        stage = 0;
        mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Clear > 2)                                   //�⺻��� ��� Ŭ������ ���Ѹ�� �ְ���������
        {
            cbestScoreTxt.text = "���Ѹ�� �ְ����� : " + bestScore.ToString();
        }
        

        Text hardbtnText = hardBtn.GetComponentInChildren<Text>();
        if (Clear >= 3)
        {
            hardbtnText.text = "���� ���";
        }
        else
        {
            hardbtnText.text = "���� ���(���)";
        }

        if (mode == 1)
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


        if (bestScore >= 20f)                        //�����ư ���Ѹ�� ����10�̻�� ����
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

        // BGM���� ���� ����
        if (stage == 4 && !AudioManager.instance.IsHellMode())
        {
            AudioManager.instance.SwitchMusic(true);
            AudioManager.instance.StopTickSfx();
        }

        //���� ��� ���� BGM ���
        if (mode == 2 && !AudioManager.instance.IsHellMode())
        {
            AudioManager.instance.SwitchMusic(false, true);
            AudioManager.instance.StopTickSfx();
        }

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (AudioManager.instance.audioSource.pitch == 1.0f && AudioManager.instance.SFXSource.isPlaying)
        {
            AudioManager.instance.StopTickSfx();
        }

        if (time <= 20.0f)
        {
            if (AudioManager.instance.audioSource.pitch != 1.5f)
            {
                AudioManager.instance.SetSpeed(1.5f);
            }

            if ((mode == 2 || stage == 4))
            {
                if (!AudioManager.instance.SFXSource.isPlaying || AudioManager.instance.SFXSource.clip != AudioManager.instance.tickSfx)
                {
                    AudioManager.instance.PlayTickSfx();
                }
            }
        }

        if (mode == 1 && time <= 0.0f && cardCount != 0)
        {
            failPanel.SetActive(true);
            Time.timeScale = 1.0f;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.StopTickSfx();
            isGamePlaying = false;
            board.gameObject.SetActive(false);
        }

        if (cardCount <= 0 && mode == 1)
        {//���� ���� ���� Ȯ��
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.StopTickSfx();

            board.gameObject.SetActive(false);

        }

        if ((cardCount == 0 && mode == 1) || (time <= 0 && mode == 2))
        {
            timeTxt.gameObject.SetActive(false);

            GameClear(stage,score);

            Delete();
        }

        if (AudioManager.instance.audioSource.pitch == 1.0f && AudioManager.instance.SFXSource.isPlaying)
        {
            AudioManager.instance.StopTickSfx();
        }

        Option();

        if (board.gameObject.activeSelf == true)
        {
            optionButton.gameObject.SetActive(true);
        }
        else
        {
            optionButton.gameObject.SetActive(false);
        }

    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            if (secondCard.idx == 8) // �Ƿ� ī�尡 ��ġ�Ǿ��� ��
            {
                isGhost = true; //isGhost true -> ī�� ������ �Ұ���
                firstCard.DestroyCard(0f);

                secondCard.shouldTurnOffGhost = true; // �տ� firstcard�� �ı��Ǿ��� �� isGhost�� false�� ���� �ʵ��� �����ϴ� ����
                secondCard.front.GetComponent<Animator>().SetBool("isOpen", true); // �Ƿ� Ȯ�� �ִ� ����
                secondCard.transform.position = new Vector2(0, 0); //�Ƿ� �߾����� �̵���Ŵ
                secondCard.DestroyCard(3f); //3�� �� ī�� �ı�
            }
            else
            {
                firstCard.DestroyCard(1f);
                secondCard.DestroyCard(1f);
            }

            cardCount -= 2;

            
            if (mode == 2)
            {
                score++;
                if (cardCount == 0)
                {
                    Invoke("ReBoard", 1f);
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
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();

        int pastBestScore = PlayerPrefs.GetInt("bestScore", bestScore); // ���Ѹ�� �ְ�����
        int pastStage = PlayerPrefs.GetInt("Clear", Clear); // ���� �ְ� ��������
        Time.timeScale = 0.0f;

        //if (stage >= 0 && stage <= 2)
        //{
        //    nextPanel.SetActive(true);
        //}

        //else if (stage == 3)
        //{
        //    clearPanel.SetActive(true);
        //}

        if (stage == 4)
        {
            hiddenPanel.SetActive(true);
        }

        if (mode == 1)
        {
            Clear = _stage;

            //if (stage == 4)
            //{
            //    clearPanel.SetActive(true);
            //}

            if (stage >= 0 && stage <= 2)
            {
                nextPanel.SetActive(true);
            }

            else if (stage == 3)
            {
                clearPanel.SetActive(true);
            }

        }
        else if (mode == 2)
        {
            Clear = _stage + 3;
            board.OnDisable();

            infinityPanel.SetActive(true);

            if (score >= pastBestScore)
            {
                PlayerPrefs.SetInt("bestScore", score);  
                PlayerPrefs.Save(); 

                bestScoreTxt.text = score.ToString();
                nowScoreTxt.text = score.ToString();
            }

            else
            {
                bestScoreTxt.text = bestScore.ToString();
                nowScoreTxt.text = score.ToString();
            }

            //score = 0;
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
        if (mode == 2)
        {
            //score = 0;  // ���Ѹ�� ���� �ʱ�ȭ
        }
    }

    private void ReBoard()
    {
        board.Start();
    }

    private void Option()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && board.gameObject.activeSelf == true)
        {
            if (optionPanel.activeSelf == false)
            {
                optionPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (optionPanel.activeSelf == true)
            {
                optionPanel.SetActive(false);
                Time.timeScale = 1f;
            }

        }
    }
}
