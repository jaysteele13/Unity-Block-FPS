using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWeight : MonoBehaviour
{
    [HideInInspector]
    public static Object instance;


    public Vector3 forceToAdd;
    public Rigidbody theRB;
    private Vector3  objectTransform;
 

    public void Awake()
    {
        instance = this; 
    }

    public void Start()
    {
        objectTransform = transform.position; 
    }
    public void Update()
    {
        if(forceToAdd != Vector3.zero)
        {
            theRB.AddForce(forceToAdd);
            forceToAdd = Vector3.zero;
        }

        if (Vector3.Distance(transform.position, Vector3.zero) <= 0)
        {
            objectTransform = Vector3.zero;
            theRB.isKinematic = true;
        }
        else
        {
            theRB.isKinematic = false; 
        }
    }


    public void CheckRocket(Vector3 rocketPos, float dist, float force)
    {
        forceToAdd = (transform.position - rocketPos).normalized * Mathf.Lerp(0, force, 1 - (Vector3.Distance(transform.position, rocketPos)/dist));
        //LocationInfo of game object(cube) - cube points at direction
        
    }

    





}
