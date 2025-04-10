using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBg : MonoBehaviour
{
    public float scrollSpeed = 0.4f;

    public Vector3 resetPos = new Vector3(2.34f, 2.34f);   // 반복되는 경계 위치
    public Vector3 startPos = new Vector3(-3f, -3f); // 시작 위치

    void Start()
    {
        transform.position = startPos;
    }
    void Update()
    {
        // 오른쪽 위로 이동 (X, Y 둘 다 양수)
        transform.Translate(new Vector3(1, 1, 0).normalized * scrollSpeed * Time.deltaTime);

        // 리셋 위치보다 크면 되돌리기
        if (transform.position.x >= resetPos.x && transform.position.y >= resetPos.y)
        {
            transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
        }
    }
}
