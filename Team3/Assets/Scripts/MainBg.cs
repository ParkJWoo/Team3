using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBg : MonoBehaviour
{
    public float scrollSpeed = 0.4f;

    public Vector3 resetPos = new Vector3(2.34f, 2.34f);   // �ݺ��Ǵ� ��� ��ġ
    public Vector3 startPos = new Vector3(-3f, -3f); // ���� ��ġ

    public float fadeSpeed = 2.0f; // ���� ��ȭ �ӵ�
    public float cycleSpeed = 4.0f;

    private SpriteRenderer sr;
    private float alphaTime = 0f;

    public bool fadeActive = false;
    void Start()
    {
        transform.position = startPos;
        sr = GetComponent<SpriteRenderer>();
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
        if (fadeActive)
        {
            alphaTime += Time.deltaTime * cycleSpeed;
            float alpha = 0.75f + Mathf.Sin(alphaTime) * 0.25f * fadeSpeed;        //Mathf.Sin(alphaTime)�� -1~1 �׷���
            Color c = sr.color;
            c.a = Mathf.Clamp(alpha, 0f, 1f);
            sr.color = c;
        }
    }


    public void ActivateFade()
    {
        fadeActive = true;
    }
}