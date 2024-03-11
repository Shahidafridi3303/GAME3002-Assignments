using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Text m_MissedBalloonText;
    private float m_fMissedBalloons = 0;
    [SerializeField] private AudioSource MissedBalloonSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Destroy(other.gameObject);
            UpdateScoreText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void UpdateScoreText()
    {
        m_fMissedBalloons++;
        MissedBalloonSFX.Play();
        m_MissedBalloonText.color = Color.magenta;
        m_MissedBalloonText.text = "Missed Balloons: " + m_fMissedBalloons + "/5";

        if (m_fMissedBalloons == 5)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
