using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject playAgainButton;
    public GameObject resumeButton;
    public TextMeshProUGUI text;
    private float total;
    private float required; 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);        
        playAgainButton.gameObject.SetActive(false);
        total = 0;
        UpdateScore(0);
        required = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.gameObject.SetActive(true);
        }
        if(total == required)
        {
            ShowWinScreen();
        }
    }

    public void ClickResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void ClickPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClickBackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateScore(float amount)
    {
        total += amount;
        text.text = "Souls " + total.ToString() + "/10";
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(true);
    }
}
