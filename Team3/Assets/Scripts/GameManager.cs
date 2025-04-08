using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Card firstCard;
    public Card secondCard;

    AudioSource audioSource;
    public AudioClip clip;

    public GameObject clearPanel;                   //  게임 최종 클리어 시 나오는 팀원 정보 판넬
    public GameObject nextStagePanel;               //  스테이지 클리어 시 나오는 다음 스테이지 선택 판넬
    public GameObject failPanel;

    public Text timeTxt;
    float time = 5.0f;

    public int cardCount = 16;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time <= 0.0f)
        {
            Time.timeScale = 0.0f;
            failPanel.SetActive(true);                            //  제한 시간 내 카드를 모두 맞추지 못할 경우, 실패 판넬 생성 및 시간 정지
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

            if(cardCount == 0)
            {
                nextStagePanel.SetActive(true);                       //  카드를 모두 맞췄을 시, 최종 스테이지가 아닐 시, 다음 스테이지 판넬 생성
                //clearPanel.SetActive(true);                         //  카드를 모두 맞췄을 시, 최종 스테이지일 시, 클리어 판넬 생성
                Time.timeScale = 0.0f;
            }
        }

        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
