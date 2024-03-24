using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float torqueMagnitude = 500f; // Adjust this value to control the torque applied to the ball
    public float forceMagnitude = 10f;   // Adjust this value to control the force applied to the ball

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 direction = collision.contacts[0].point - transform.position;

                // Apply torque to the ball
                ballRigidbody.AddTorque(Vector3.Cross(direction, Vector3.up) * torqueMagnitude, ForceMode.Impulse);

                // Apply force to the ball in the direction away from the bumper
                ballRigidbody.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
