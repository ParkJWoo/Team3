using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject Card;

    void Start()
    {
        if (GameManager.instance.stage == 1)
        {
            List<int> cardArr = RandomCard(2);

            for (int i = 0; i < cardArr.Count; i++) //카드 배치
            {
                GameObject instCard = Instantiate(Card, this.transform);

                float x = (i % 2) * 1.4f - 2.1f;
                float y = (i / 2) * 1.4f - 3.0f;

                instCard.transform.position = new Vector2(x, y);
                instCard.GetComponent<Card>().Setting(cardArr[i]);
            }

            GameManager.instance.cardCount = cardArr.Count;
        }

        if (GameManager.instance.stage == 2)
        {
            List<int> cardArr = RandomCard(4);

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

        if (GameManager.instance.stage == 3)
        {
            List<int> cardArr = RandomCard(8);

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
    }

    private List<int> RandomCard(int cardCount) // 카드 이미지의 랜덤성 부여
    {
        List<int> allCards = Enumerable.Range(0, 8).ToList(); // 0~8까지 연속된 정수 리스트 생성
        allCards = allCards.OrderBy(x => Random.Range(0f, 1f)).Take(cardCount).ToList(); // 리스트를 랜덤하게 재배치하고 원하는 카드의 갯수 지정

        List<int> pair = new List<int>();
        foreach (int card in allCards) // 지정한 카드를 pair리스트에 2개씩 추가 (배열은 크기가 고정, 동적 추가 X))
        {
            pair.Add(card);
            pair.Add(card);
        }

        pair = pair.OrderBy(x => Random.Range(0f, 1f)).ToList(); // pair리스트에 추가한 카드를 랜덤하게 재배치

        return pair;
    }
}