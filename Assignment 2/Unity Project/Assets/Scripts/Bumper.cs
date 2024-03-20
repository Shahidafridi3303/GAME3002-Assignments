using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    // Adjust this force value to control the strength of the push
    public float m_fForceModifier = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the ball
        if (collision.gameObject.tag == "Ball")
        {
            float force = -collision.gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude * m_fForceModifier;
            Vector3 contactPosition = collision.GetContact(0).point;
            Vector3 direction = collision.gameObject.transform.position - contactPosition;
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction * force, contactPosition, ForceMode.Impulse);
        }
    }
}
