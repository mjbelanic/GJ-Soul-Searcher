using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void ClickStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickHowToPlayButton()
    {
        SceneManager.LoadScene("How To Play");
    }

    public void ClickBackToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }
}
