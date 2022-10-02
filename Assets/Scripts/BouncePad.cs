using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.Bounce(bounceForce);
            Debug.Log("we bouncing");

            UIController.instance.ShowBounce();

            AudioManager.instance.PlaySFX(0);
            AudioManager.instance.PlaySFX(9);
        }
    }
}
