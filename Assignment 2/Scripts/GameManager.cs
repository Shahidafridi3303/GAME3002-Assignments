using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ball, startButton, highScoreText, scoreText, 
        quitButton, restartButton, BGImage1, BGImage2, Instructions;

    int score, highScore;


    [SerializeField]
    Vector3 startPos;

    public int multiplier;

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

        Time.timeScale = 1;
        score = 0;
        multiplier = 1;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        highScoreText.GetComponent<Text>().text = "HighScore : " + highScore;
    }

    public void UpdateScore(int point, int mullIncrease)
    {
        multiplier += mullIncrease;
        score += point * multiplier;
        scoreText.GetComponent<Text>().text = "Score : " + score;
    }

    public void GameEnd()
    {
        Time.timeScale = 0;
        highScoreText.SetActive(true);
        quitButton.SetActive(true);
        restartButton.SetActive(true);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        highScoreText.GetComponent<Text>().text = "HighScore : " + highScore;
    }

    public void GameStart()
    {
        highScoreText.SetActive(true);
        startButton.SetActive(false);
        Instructions.SetActive(false);
        BGImage1.SetActive(true);
        BGImage2.SetActive(true);
        scoreText.SetActive(true);
        Instantiate(ball, startPos, Quaternion.identity);
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
}
