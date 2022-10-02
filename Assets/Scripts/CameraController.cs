using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    private float startFOV, targetFOV; // start - default Field of View, target - how far it zooms in 

    public float zoomSpeed = 5f;

    public Camera theCam;

    private void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV; 
    }

    private void Awake()
    {
        instance = this; 
    }

    void LateUpdate() //late update stops jitters
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime); //makes value move to certain value in own time, halfing every frame for smooth effect
    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom; 
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;    
    }
}
