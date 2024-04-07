using UnityEngine;

public class Torque : MonoBehaviour
{
    public float torqueAmount = 100f; // Adjust torque amount as needed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calculate the absolute angular velocity
        float angularSpeed = Mathf.Abs(rb.angularVelocity);

        // Add torque to the Rigidbody2D to make it rotate
        rb.AddTorque(torqueAmount * Mathf.Sign(angularSpeed));
    }
}
