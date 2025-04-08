using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject levelPanel;
    public void LetGameStart()
    {
        if (GameManager.instance.stage != 0 && GameManager.instance.closeSpeed != 0)
        {
            levelPanel.SetActive(false);
            GameManager.instance.board.gameObject.SetActive(true);
            GameManager.instance.timeTxt.gameObject.SetActive(true);
        }
    }
}
