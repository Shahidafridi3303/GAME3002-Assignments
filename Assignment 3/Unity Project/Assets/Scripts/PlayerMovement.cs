using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7.4f;
    [SerializeField] private float JumpForce = 10f;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    [SerializeField] private float distance = 7.4f;
    [SerializeField] private float speed2 = 15.0f;

    //[SerializeField] private float strikeCooldown = 1f; // Cooldown time for strike in seconds
    //private bool canStrike = true;

    private void Awake()
    {
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, JumpForce);
            anim.SetTrigger("jump");
            grounded = false;
        }

        if (Input.GetKey(KeyCode.M))
        {
            Strike();
            //StartCoroutine(StrikeWithCooldown());
        }

        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    //private IEnumerator StrikeWithCooldown()
    //{
    //    Strike();

    //    Set canStrike to false to prevent further strikes during cooldown
    //    canStrike = false;

    //    Perform the strike
    //    Strike();

    //    Wait for the cooldown duration

    //   yield return new WaitForSeconds(strikeCooldown);

    //    Set canStrike to true to allow striking again
    //    canStrike = true;
    //}

    private void Strike()
    {
        // Calculate the target position for the strike move
        Vector2 strikeTarget = body.position + Vector2.right * distance;

        //// Clamp the target position to stay within the game boundaries
        //strikeTarget.x = Mathf.Clamp(strikeTarget.x, minX, maxX);

        // Calculate the velocity required to reach the target position
        Vector2 strikeVelocity = (strikeTarget - body.position) / Time.fixedDeltaTime;

        // Adjust the velocity if it exceeds the maximum speed
        //if (strikeVelocity.magnitude > speed)
        //{
        strikeVelocity = strikeVelocity.normalized * speed2;
        //}

        // Apply the strike velocity directly to the rigidbody
        body.velocity = strikeVelocity;
        transform.localScale = Vector3.one;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}

