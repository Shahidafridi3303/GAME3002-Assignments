using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plunger : MonoBehaviour
{
    float currentPower;
    public float strength = 3f;
    float minPower = 0f;
    public float maxPower = 100f;
    public Slider powerSlider;
    Rigidbody ballRigidbody;
    bool ballReady;
    PhysicMaterial originalMaterial; // Store the original physics material

    // Start is called before the first frame update
    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        powerSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }

        powerSlider.value = currentPower;

        if (ballRigidbody != null && ballReady)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (currentPower <= maxPower)
                {
                    currentPower += 50 * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ballRigidbody.AddForce(currentPower * strength * Vector3.forward);
                currentPower = minPower;
            }
        }
        else
        {
            currentPower = minPower;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Collider ballCollider = other.GetComponent<Collider>();
            if (ballCollider != null)
            {
                originalMaterial = ballCollider.material; // Store the original material
                PhysicMaterial newMaterial = new PhysicMaterial();
                newMaterial.bounciness = 0f;
                ballCollider.material = newMaterial; // Set the new material with zero bounciness
            }
            ballReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Collider ballCollider = other.GetComponent<Collider>();
            if (ballCollider != null)
            {
                ballCollider.material = originalMaterial; // Restore the original material
            }
            ballRigidbody = null;
            ballReady = false;
            currentPower = minPower;
        }
    }
}
