using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 7.4f;
    private float currentSpeed; 
    [SerializeField] private float baseJumpForce = 10f;
    private float currentJumpForce;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;
    private bool canDoubleJump;

    [SerializeField]
    GameObject KeyCollectedText;

    private bool IsKeyCollected = false;

    private GameManager gameManager;

    // For teleport
    [SerializeField]
    Transform teleportTarget1, teleportTarget2, teleportTarget3, teleportTarget4;

    private void Awake()
    {
        gameManager = GameManager.instance;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = baseSpeed;
        currentJumpForce = baseJumpForce;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * currentSpeed, body.velocity.y);

        //Flipping the player when facing left/right.
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                canDoubleJump = true;
                body.velocity = new Vector2(body.velocity.x, currentJumpForce);
                anim.SetTrigger("jump");
                grounded = false;
            }
            
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                body.velocity = new Vector2(body.velocity.x, currentJumpForce);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = teleportTarget1.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = teleportTarget2.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = teleportTarget3.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = teleportTarget4.position;
        }

        //setting animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Ground":
                grounded = true;
                break;

            case "key":
                Destroy(collision.gameObject);
                IsKeyCollected = true;
                KeyCollectedText.SetActive(false);
                break;

            case "Time":
                gameManager.remainingTime += 10f; // Increase remainingTime by 10
                Destroy(collision.gameObject);
                break;

            case "Door":
                if (IsKeyCollected == true)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    KeyCollectedText.SetActive(true);
                }
                break;

            case "Spikes":
                Time.timeScale = 0;
                gameManager.GameOver();
                Destroy(gameObject);
                break;

            case "Flag":
                LoadNextLevel();
                break;

            case "restartFlag":
                LoadNextLevel();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUpZone") || other.CompareTag("SlowDownZone"))
        {
            if (other.CompareTag("SpeedUpZone"))
            {
                currentSpeed = baseSpeed * 2.0f;
                currentJumpForce = baseJumpForce * 1.5f;
            }
            else
            {
                currentSpeed = baseSpeed * 0.5f;
                currentJumpForce = baseJumpForce;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUpZone") || other.CompareTag("SlowDownZone"))
        {
            currentSpeed = baseSpeed;
            currentJumpForce = baseJumpForce;
        }
    }

    public void LoadNextLevel()
    {
        // Getting the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Loading the next build level
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}

