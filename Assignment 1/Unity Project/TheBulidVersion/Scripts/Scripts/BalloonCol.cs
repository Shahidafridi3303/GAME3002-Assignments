using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCol : MonoBehaviour
{
    private BalloonController BalloonSpawn;

    void Awake()
    {
        BalloonSpawn = GameObject.Find("Plane").GetComponent<BalloonController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Plane" & col.gameObject.name != "Present(Clone)")
        {
            GameObject.Destroy(col.gameObject);
            BalloonSpawn.IncrementScore();
            GameObject.Destroy(this.gameObject);
        }
    }
}
