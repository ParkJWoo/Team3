using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public float closeSpeed;

    public void LevelBtn()
    {
        GameManager.instance.closeSpeed = closeSpeed;
    }
}
