using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject levelPanel;
    public void LetGameStart()
    {
        levelPanel.SetActive(false);
        GameManager.instance.board.gameObject.SetActive(true);
        GameManager.instance.timeTxt.gameObject.SetActive(true);
    }
}