using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 7.4f;
    private float currentSpeed; 

    [SerializeField] private float JumpForce = 10f;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    private bool canDoubleJump;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = baseSpeed;
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
                body.velocity = new Vector2(body.velocity.x, JumpForce);
                anim.SetTrigger("jump");
                grounded = false;
            }
            
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                body.velocity = new Vector2(body.velocity.x, JumpForce);
            }
        }

        //setting animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUpZone") || other.CompareTag("SlowDownZone"))
        {
            if (other.CompareTag("SpeedUpZone"))
            {
                currentSpeed = baseSpeed * 2.0f;
            }
            else
            {
                currentSpeed = baseSpeed * 0.5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUpZone") || other.CompareTag("SlowDownZone"))
        {
            currentSpeed = baseSpeed;
        }
    }
}

