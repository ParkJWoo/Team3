using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject EndTxt;

    AudioSource audioSource;
    public AudioClip clip;

    float time = 0.0f;

    public int cardCount = 0;

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
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
<<<<<<< HEAD

        if (time > 30.0f)
        {
            EndTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
=======
>>>>>>> parent of 2ab9a5d ([박진우] GameManager에 클리어 판넬 생성 조건 추가)
    }
    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
<<<<<<< HEAD
            cardCount -= 2;
            if (cardCount == 0)
            {
                Time.timeScale = 0.0f;
                EndTxt.SetActive(true);
            }
=======
>>>>>>> parent of 2ab9a5d ([박진우] GameManager에 클리어 판넬 생성 조건 추가)
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
