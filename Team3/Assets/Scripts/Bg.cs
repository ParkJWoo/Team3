using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg : MonoBehaviour
{
    public float scrollSpeed = 0.4f;
    public float resetX = -2.71f;   // 반복되는것처럼 보이는위치
    public float startX = -0.03f;    // 다시 시작할 위치

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= resetX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startX;
            transform.position = newPos;
        }
    }
}
