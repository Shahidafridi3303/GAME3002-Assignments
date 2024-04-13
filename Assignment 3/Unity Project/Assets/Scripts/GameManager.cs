using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject startButton, keyText,
    quitButton, restartButton, BGImage, Instructions;

    public static GameManager instance;
    int key = 0;

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

    public void UpdateKeyScore()
    {
        key += 1;
        keyText.GetComponent<Text>().text = "Key : " + key + "/2";
    }

    public void GameEnd()
    {
        quitButton.SetActive(true);
        restartButton.SetActive(true);
    }

    public void GameStart()
    {
        startButton.SetActive(false);
        Instructions.SetActive(false);
        BGImage.SetActive(false);
        keyText.SetActive(true);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
