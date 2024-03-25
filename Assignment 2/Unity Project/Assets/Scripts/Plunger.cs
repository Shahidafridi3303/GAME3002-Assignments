using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plunger : MonoBehaviour
{
    float m_fCurrentPower;
    public float m_fStrength = 3f;
    float m_fMinPower = 0f;
    public float m_fMaxPower = 100f;
    public Slider powerSlider;
    Rigidbody ballRigidbody;

    [SerializeField]
    GameObject PlungerBox, PlungerPump;

    bool ballReady;
    PhysicMaterial originalMaterial; 

    // Start is called before the first frame update
    void Start()
    {
        powerSlider.minValue = m_fMinPower;
        powerSlider.maxValue = m_fMaxPower;
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

        powerSlider.value = m_fCurrentPower;

        if (ballRigidbody != null && ballReady)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (m_fCurrentPower <= m_fMaxPower)
                {
                    m_fCurrentPower += 50 * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ballRigidbody.AddForce(m_fCurrentPower * m_fStrength * Vector3.forward);
                m_fCurrentPower = m_fMinPower;
            }
        }
        else
        {
            m_fCurrentPower = m_fMinPower;
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
                originalMaterial = ballCollider.material; 
                PhysicMaterial newMaterial = new PhysicMaterial();
                newMaterial.bounciness = 0f;
                ballCollider.material = newMaterial; // in order to set ball's bouncy initially zero
            }
            ballReady = true;

            PlungerBox.GetComponent<Renderer>().material.color = Color.yellow;
            PlungerPump.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Collider ballCollider = other.GetComponent<Collider>();
            if (ballCollider != null)
            {
                ballCollider.material = originalMaterial; 
            }
            ballRigidbody = null;
            ballReady = false;
            m_fCurrentPower = m_fMinPower;

            PlungerBox.GetComponent<Renderer>().material.color = Color.green;
            PlungerPump.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
