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

        if (GameManager.instance.stage == 4)
        {
            Stage4BoardArrangement();
        }
    }

    public void Stage1BoardArrangement() //  �������� ����â���� ������ �������� ������ ������� ī�� ��ġ�ϴ� �Լ�
    {
        Debug.Log(GameManager.instance.stage);

        List<int> cardArr = RandomCard(2);
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 7f)).ToList(); // �迭 ���� ����

        for (int i = 0; i < cardArr.Count; i++) //ī�� ��ġ
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 2) * 1.4f - 2.1f;
            float y = (i / 2) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }
        
        GameManager.instance.cardCount = cardArr.Count;
    }

    public void Stage2BoardArrangement()
    {
        List<int> cardArr = RandomCard(4);
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 7f)).ToList(); // �迭 ���� ����

        for (int i = 0; i < cardArr.Count; i++) //ī�� ��ġ
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    public void Stage3BoardArrangement()
    {
        List<int> cardArr = RandomCard(8);
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 7f)).ToList(); // �迭 ���� ����

        for (int i = 0; i < cardArr.Count; i++) //ī�� ��ġ
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
        List<int> cardArr = RandomCard(10);
        cardArr = cardArr.OrderBy(x => Random.Range(0f, 9f)).ToList(); // �迭 ���� ����

        for (int i = 0; i < cardArr.Count; i++) //ī�� ��ġ
        {
            GameObject instCard = Instantiate(Card, this.transform);

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.5f;

            instCard.transform.position = new Vector2(x, y);
            instCard.GetComponent<Card>().Setting(cardArr[i]);
        }

        GameManager.instance.cardCount = cardArr.Count;
    }

    private List<int> RandomCard(int cardCount) // ī�� �̹����� ������ �ο�
    {
        List<int> allCards = Enumerable.Range(0, cardCount).ToList(); // 0~8���� ���ӵ� ���� ����Ʈ ����
        allCards = allCards.OrderBy(x => Random.Range(0f, 1f)).Take(cardCount).ToList(); // ����Ʈ�� �����ϰ� ���ġ�ϰ� ���ϴ� ī���� ���� ����

        List<int> pair = new List<int>();
        foreach (int card in allCards) // ������ ī�带 pair����Ʈ�� 2���� �߰�
        {
            pair.Add(card);
            pair.Add(card);
        }

        pair = pair.OrderBy(x => Random.Range(0f, 1f)).ToList(); // pair����Ʈ�� �߰��� ī�带 �����ϰ� ���ġ

        return pair;
    }
}