using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;

    public List<int> cardArr = new List<int>();

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

        if (GameManager.instance.stage == 4)
        {
            Stage4BoardArrangement();
        }
    }

    public void Stage1BoardArrangement() //  스테이지 선택창에서 선택한 스테이지 데이터 기반으로 카드 배치하는 함수
    {
        Debug.Log(GameManager.instance.stage);

        //List<int> cardArr = RandomCardImg(2);
        cardArr = RandomCardImg(2);

        for (int i = 0; i < cardArr.Count; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 2) * 1.4f - 0.8f;
            float y = (i / 2) * 1.4f - 1.3f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    public void Stage2BoardArrangement()
    {
        //List<int> cardArr = RandomCardImg(4);
        cardArr = RandomCardImg(2);

        for (int i = 0; i < cardArr.Count; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 3) * 1.4f - 1.4f;
            float y = (i / 3) * 1.4f - 2.4f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    public void Stage3BoardArrangement()
    {
        //List<int> cardArr = RandomCardImg(8);
        cardArr = RandomCardImg(8);

        for (int i = 0; i < cardArr.Count; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    public void Stage4BoardArrangement()
    {
        //List<int> cardArr = RandomCardImg(10);
        cardArr = RandomCardImg(10);

        for (int i = 0; i < cardArr.Count; i++) //카드 배치
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.5f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    private List<int> RandomCardImg(int cardCount) // 카드 이미지의 랜덤성 부여
    {
        List<int> cards = Enumerable.Range(0, 8).ToList(); // 0~8까지 연속된 정수 리스트 생성
        if (cardCount > 8)
            cards = Enumerable.Range(0, 10).ToList(); // 0~10까지 연속된 정수 리스트 생성

        cards = cards.OrderBy(x => Random.Range(0f, 1f)).Take(cardCount).ToList(); // 리스트를 랜덤하게 재배치하고 원하는 카드의 갯수 지정

        List<int> pair = new List<int>();
        foreach (int card in cards) // 지정한 카드를 pair리스트에 2개씩 추가
        {
            pair.Add(card);
            pair.Add(card);
        }

        pair = pair.OrderBy(x => Random.Range(0f, 1f)).ToList(); // pair리스트에 추가한 카드를 랜덤하게 재배치

        return pair;
    }

    public void Delete()
    {
        Destroy(Card.gameObject);
        cardArr.Clear();
    }
}