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

    public GameObject hiddenBtn;

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

        if (Clear >= 6f)                        //�������3���� Ŭ���������� ����
        {
            hiddenBtn.SetActive(true);
        }
        else
        {
            hiddenBtn.SetActive(false);
        }

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
        }

        if (time <= 0.0f && cardCount != 0)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0.0f;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.SwitchMusic(false);
            isGamePlaying = false;

            //time = 60.0f;
        }

        if (cardCount <= 0)
        {
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed();

            if (stage == 4)
            {
                AudioManager.instance.SwitchMusic(false);
            }

            //Time.timeScale = 1.0f;
            //time = 60.0f;
            //timeTxt.text = time.ToString("N2");
            return;
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
            if (cardCount == 0)
            {
                board.gameObject.SetActive(false);
                timeTxt.gameObject.SetActive(false);
                if (stage == 1)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    if (closeSpeed == 1)
                    {
                        Clear = 1;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
                    else
                    {
                        Clear = 4;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
                }
                else if (stage == 2)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    if (closeSpeed == 1)
                    {
                        Clear = 2;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
                    else
                    {
                        Clear = 5;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
                }
                else if (stage == 3)                           //  1��������, Ȥ�� 2�������� Ŭ���� �� ������ ���� �������� �̵� �ǳ� ���� ����
                {
                    nextPanel.SetActive(true);                         //  ī�带 ��� ������ ��, Ŭ���� �ǳ� ����
                    Time.timeScale = 0.0f;
                    if (closeSpeed == 1)
                    {
                        Clear = 3;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
                    else
                    {
                        Clear = 6;
                        PlayerPrefs.SetInt("Clear", Clear);
                    }
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
