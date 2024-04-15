using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Timer
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;
    private bool startTimer = false;

    // Other UI elements
    [SerializeField]
    GameObject pauseButton, resumeButton, startButton,
    restartLevel, restartGame, quitButton, BGImage, Instructions;
    
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

    private void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (startTimer)
        {
            StartCountdownTimer();
        }
    }

    public void Pausegame()
    {
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        restartLevel.SetActive(true);
        restartGame.SetActive(true);
        quitButton.SetActive(true);

        Time.timeScale = 0;
    }
    public void Resumegame()
    {
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        restartLevel.SetActive(false);
        restartGame.SetActive(false);
        quitButton.SetActive(false);

        Time.timeScale = 1;
    }

    public void GameStart()
    {
        startTimer = true;
        pauseButton.SetActive(true);
        startButton.SetActive(false);
        Instructions.SetActive(false);
        BGImage.SetActive(false);

        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        print("Hello");
        restartLevel.SetActive(false);
        restartGame.SetActive(false);
        quitButton.SetActive(false);

        Time.timeScale = 1;

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load the next scene
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void RestartGame()
    {
        restartLevel.SetActive(false);
        restartGame.SetActive(false);
        quitButton.SetActive(false);

        Time.timeScale = 1;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameOver()
    {
        pauseButton.SetActive(false);
        restartLevel.SetActive(true);
        restartGame.SetActive(true);
        quitButton.SetActive(true);

        //Time.timeScale = 1;
    }

    // Timer
    void StartCountdownTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;

            Time.timeScale = 0;
            GameOver();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
