using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBg : MonoBehaviour
{
    public float scrollSpeed = 0.4f;

    public Vector3 resetPos = new Vector3(2.34f, 2.34f);   // �ݺ��Ǵ� ��� ��ġ
    public Vector3 startPos = new Vector3(-3f, -3f); // ���� ��ġ

    void Start()
    {
        transform.position = startPos;
    }
    void Update()
    {
        // ������ ���� �̵� (X, Y �� �� ���)
        transform.Translate(new Vector3(1, 1, 0).normalized * scrollSpeed * Time.deltaTime);

        // ���� ��ġ���� ũ�� �ǵ�����
        if (transform.position.x >= resetPos.x && transform.position.y >= resetPos.y)
        {
            transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
        }
    }
}
