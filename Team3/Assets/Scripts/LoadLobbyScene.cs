using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLobbyScene : MonoBehaviour
{
    public void LoadStartScene()                                  //  MainScene ȣ��
    {
        SceneManager.LoadScene("MainScene");
    }
}
