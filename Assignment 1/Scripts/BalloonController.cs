using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BalloonController : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private float m_fscore = 0;
    public Text scoretext;
    public float m_fSpawnInterval = 3f;

    [SerializeField] private AudioSource ScoreSFX;

    void Start()
    {
        scoretext.color = Color.yellow;
        InvokeRepeating("SpawnObjects", 0f, m_fSpawnInterval);
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(-6.91f, 5.0f, Random.Range(-14, 14));
            GameObject obj = Instantiate(objectsToSpawn[i], spawnPosition, Quaternion.identity);
            GameObject.Destroy(obj, 8);
        }
    }

    void FixedUpdate()
    {
        GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject balloon in balloons)
        {
            balloon.transform.Translate(Vector3.down * 2f * Time.deltaTime);
        }
    }

    public void IncrementScore()
    {
        m_fscore++;
        ScoreSFX.Play();
        scoretext.color = Color.green;
        scoretext.text = "Score: " + m_fscore + "/100";
        if (m_fscore == 100)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
