using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    //when picking up weapon tell name and add to all guns list
    public string theGun;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            //give ammo
            PlayerController.instance.AddGun(theGun);

            Destroy(gameObject);
            collected = true;

            AudioManager.instance.PlaySFX(4);
        }

    }
}
