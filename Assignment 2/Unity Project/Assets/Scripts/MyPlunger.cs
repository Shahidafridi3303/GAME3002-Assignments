using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlunger : MonoBehaviour
{
    bool ball_collided = false;
    [SerializeField]
    float launchForce = 0f;

    [SerializeField]
    GameObject ball;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (ball_collided & Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("Collision detected with the ball.");
            ball_collided = true;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        }
    }

    void LaunchBall()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>(); 
        if (ballRigidbody != null)
        {
            ballRigidbody.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
            ball_collided = false;
        }
    }

}


