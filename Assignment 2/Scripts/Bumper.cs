using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float m_fTorqueMagnitude = 500f; 
    public float m_fForceMagnitude = 10f;  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 direction = collision.contacts[0].point - transform.position;

                // Apply torque to the ball
                ballRigidbody.AddTorque(Vector3.Cross(direction, Vector3.up) * m_fTorqueMagnitude, ForceMode.Impulse);

                // Apply force to the ball in the direction away from the bumper
                ballRigidbody.AddForce(direction.normalized * m_fForceMagnitude, ForceMode.Impulse);
            }
        }
    }
}
