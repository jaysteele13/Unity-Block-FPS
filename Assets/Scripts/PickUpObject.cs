using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform grabPos, ground;
    public Rigidbody theRB;

    public float thrust = 100f; 
    

   void OnMouseDown()
    {
        if (PlayerController.instance.activeGun.gunName == "fist")
        {
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = grabPos.position;

            this.transform.parent = GameObject.Find("toGrab").transform;
        }
    }

    void OnMouseUp()
    {
        this.transform.parent = ground;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<MeshCollider>().enabled = true;

        Vector3 direction = PlayerController.instance.firePoint.position - transform.position;
        theRB.AddForceAtPosition(direction.normalized, transform.position);

        theRB.AddRelativeForce(Vector3.forward * thrust);

        //this.transform.parent = null;
        



    }





}
