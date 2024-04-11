using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7.4f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float strikeCooldown = 1f; // Cooldown time for strike in seconds
    private float nextStrikeTime; // Time when the next strike can be performed
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;

    [SerializeField] private float distance = 7.4f;
    [SerializeField] private float strikeSpeed = 15.0f;

    private void Awake()
    {
        // Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        nextStrikeTime = Time.time; // Set initial next strike time
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player when facing left/right.
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
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            anim.SetTrigger("jump");
            grounded = false;
        }

        if (Input.GetKey(KeyCode.M) && Time.time >= nextStrikeTime)
        {
            Debug.Log("Strike called!");
            Strike();
            nextStrikeTime = Time.time + strikeCooldown; // Update next strike time
        }

        // Sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Strike()
    {
        // Calculate the target position for the strike move
        Vector2 strikeTarget = body.position + Vector2.right * distance;

        // Calculate the velocity required to reach the target position
        Vector2 strikeVelocity = (strikeTarget - body.position) / Time.fixedDeltaTime;

        // Adjust the velocity if it exceeds the maximum speed
        // if (strikeVelocity.magnitude > speed)
        // {
        strikeVelocity = strikeVelocity.normalized * strikeSpeed;
        // }

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
