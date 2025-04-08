using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;

    public void Start()
    {
        if (GameManager.instance.stage == 1)
        {
            Stage1BoardArrangement();
        }

        if (GameManager.instance.stage == 2)
        {
            Stage2BoardArrangement();
        }

        if (GameManager.instance.stage == 3)
        {
            Stage3BoardArrangement();
        }
    }

    public void Stage1BoardArrangement()                                      //  스테이지 선택창에서 선택한 스테이지 데이터 기반으로 카드 배치하는 함수
    {
        Debug.Log(GameManager.instance.stage);

        int[] cardArr = { 0, 0, 1, 1 };
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 1f)).ToArray(); // 배열 랜덤 정렬

        for (int i = 0; i < cardArr.Length; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 2) * 1.4f - 2.1f;
            float y = (i / 2) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }
        
        GameManager.instance.cardCount = cardArr.Length;
    }

    public void Stage2BoardArrangement()
    {
        int[] cardArr = { 0, 0, 1, 1, 2, 2, 3, 3 };
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 3f)).ToArray(); // 배열 랜덤 정렬

        for (int i = 0; i < cardArr.Length; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Length;
    }

    public void Stage3BoardArrangement()
    {
        int[] cardArr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 7f)).ToArray(); // 배열 랜덤 정렬

        for (int i = 0; i < cardArr.Length; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Length;
    }

    public void Delete()
    {
        //Destroy(this.gameObject);
    }
}