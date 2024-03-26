using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Dead":
                GameManager.instance.GameEnd();
                print("Dead");

                break;

            case "Bouncer":
                GameManager.instance.UpdateScore(10, 1);
                print("bouncer");
                break;

            case "Point":
                GameManager.instance.UpdateScore(20, 1);
                print("Point");

                break;

            case "Side":
                GameManager.instance.UpdateScore(10, 0);
                print("Side");

                break;

            case "Flipper":
                GameManager.instance.multiplier = 1;
                print("Flipper");

                break;

            default:
                break;
        }
    }
}
