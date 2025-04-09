using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UIImg = UnityEngine.UI.Image;

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
        Text hardbtnText = hardBtn.GetComponentInChildren<Text>();
        if (Clear >= 3)
        {
            hardbtnText.text = "�����";
        }
        else
        {
            hardbtnText.text = "�����(���)";
        }

        if (closeSpeed == 1f)
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
        else if (closeSpeed == 0.5f)
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
        }


        if (Clear >= 6f)                        //�����ư �������3���� Ŭ���������� ����
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

        if (time <= 0.0f && cardCount != 0)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0.0f;
            AudioManager.instance.ResetSpeed(); //���󺹱�
            isGamePlaying = false;

            //time = 60.0f;
        }

        if (cardCount <= 0)
        {//���� ���� ���� Ȯ��
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed(); //���󺹱�
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
            firstCard.DestroyCard();
            secondCard.DestroyCard();

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
