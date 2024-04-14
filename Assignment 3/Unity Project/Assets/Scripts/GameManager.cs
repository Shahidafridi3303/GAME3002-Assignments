using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Timer
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private bool startTimer = false;

    // Other UI elements
    [SerializeField]
    GameObject startButton,
    quitButton, restartButton, BGImage, Instructions;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Instructions.SetActive(true);
    }

    void Update()
    {
        if (startTimer)
        {
            StartCountdownTimer();
        }
    }

    public void GameEnd()
    {
        quitButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void GameStart()
    {
        startTimer = true;
        startButton.SetActive(false);
        Instructions.SetActive(false);
        BGImage.SetActive(false);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    // Timer
    void StartCountdownTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;

            Time.timeScale = 0;
            GameEnd();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
