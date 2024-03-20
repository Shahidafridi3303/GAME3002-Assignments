using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_speed : MonoBehaviour
{
    [SerializeField] private float stableSpeed = 5f;
    private Rigidbody rb;
    private void Update()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        //rb.velocity = rb.velocity.normalized * stableSpeed;
    }
}
