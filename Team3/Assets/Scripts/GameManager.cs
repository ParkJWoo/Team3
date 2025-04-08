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
    public float time = 0.0f;

    // 스테이지 클리어 여부 판단.
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
        {//20초 지나면 bgm 속도 증가
            AudioManager.instance.SetSpeed(1.5f);
        }

        if (cardCount <= 0)
        {//게임 끝 조건 확인
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
