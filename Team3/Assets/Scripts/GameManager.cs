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

    public GameObject clearPanel; // 최종 스테이지 클리어 시 나오는 팀원 정보 판넬
    public GameObject nextPanel;  // 최종 스테이지가 아닌 스테이지 클리어 시 나오는 다음 스테이지 이동 판넬
    public GameObject failPanel;  // 제한 시간이 지날 시 나오는 실패 판넬

    public GameObject hiddenBtn;

    public Text timeTxt;
    public float time = 60.0f;

    public bool isGamePlaying = false; //게임 시작 여부 판단

    public int cardCount = 16;

    // 스테이지 클리어 여부 판단.
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

        if (Clear >= 6f)                        //어려움스테3까지 클리어했을때 보임
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
                if (stage == 1)                           //  1스테이지, 혹은 2스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    nextPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 클리어 판넬 생성
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
                else if (stage == 2)                           //  1스테이지, 혹은 2스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    nextPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 클리어 판넬 생성
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
                else if (stage == 3)                           //  1스테이지, 혹은 2스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    nextPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 클리어 판넬 생성
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

    public void Delete()                                                //  스테이지 종료 후, 기존의 스테이지 데이터 초기화 → 다음 스테이지의 데이터를 새로 받아오기 위한 정리 함수
    {
        Debug.Log("이전 스테이지 정보 삭제");
        cardCount = 0;
        //stage = 0;
        Time.timeScale = 1f;
        time = 0.0f;
        timeTxt.text = time.ToString("N2");
        //board.Delete();
    }
}
