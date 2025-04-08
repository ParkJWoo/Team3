using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public int stage;
    public int cardCount;
    public void StageBtn()
    {
        GameManager.instance.stage = stage;
        GameManager.instance.cardCount = cardCount;
    }
}
