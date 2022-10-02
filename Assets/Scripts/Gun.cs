using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public bool canAutoFire;
    public float fireRate;

    [HideInInspector] //hide it in inspector access it from other scripts
    public float fireCounter;

    public int currentAmmo, pickupAmount;


    public Transform firepoint;
    public float zoomAmount;


    public string gunName;

    // Start is called before the first frame update
    void Start()
    { 
        UIController.instance.ammoText.text = "" + currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmo()
    {

        currentAmmo += pickupAmount;


        UIController.instance.ammoText.text = "" + currentAmmo;
    }
}
