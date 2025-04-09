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

    public GameObject clearPanel; // 최종 스테이지 클리어 시 나오는 팀원 정보 판넬
    public GameObject nextPanel;  // 최종 스테이지가 아닌 스테이지 클리어 시 나오는 다음 스테이지 이동 판넬
    public GameObject failPanel;  // 제한 시간이 지날 시 나오는 실패 판넬

    public Text timeTxt;
    public float time = 60.0f;

    public bool isGamePlaying = false; //게임 시작 여부 판단

    public int cardCount = 16;

    // 스테이지 클리어 여부 판단.
    public bool isClear_1 = false;
    public bool isClear_2 = false;
    public bool isClear_3 = false;

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
        if (!isGamePlaying)
        {
            return;
        }

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        float remainingTime = 30f - time;

        //if (time >= 30.0f)
        //{
        //    failPanel.SetActive(true);
        //    Time.timeScale = 0.0f;
        //}

        if (time <= 20.0f && AudioManager.instance.audioSource.pitch == 1.0f)
        {//남은 시간이 20초 이하일 때, 오디오 속도 증가
            AudioManager.instance.SetSpeed(1.5f);
        }

        if (time <= 0.0f && cardCount != 0)
        {
            failPanel.SetActive(true);
            Time.timeScale = 0.0f;
            AudioManager.instance.ResetSpeed(); //원상복귀
            isGamePlaying = false;

            //time = 60.0f;
        }

        if (cardCount <= 0)
        {//게임 끝난 조건 확인
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed(); //원상복귀
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

                if(stage == 1 )                           //  1스테이지, 혹은 2스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    nextPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 클리어 판넬 생성
                    Time.timeScale = 0.0f;
                    isClear_1 = true;
                }

                if (stage == 2)                           //  1스테이지, 혹은 2스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    nextPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 클리어 판넬 생성
                    Time.timeScale = 0.0f;
                    isClear_2 = true;
                }

                else if(stage == 3)                                      //  최종 스테이지 클리어 시 나오는 다음 스테이지 이동 판넬 생성 로직
                {
                    clearPanel.SetActive(true);
                    Time.timeScale = 0.0f;
                    isClear_3 = true;
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
