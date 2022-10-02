using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkThroughTutorial : MonoBehaviour
{
    public static WalkThroughTutorial instance;

    public float waitTime = 2f;
    public GameObject blockade;

    [HideInInspector]
    public bool tutorialTriggered, fistTriggered;
    
    

    public void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameObject.name == "Tutorial")
        {
            tutorialTriggered = true;
            Debug.Log("we tutorial");

            UIController.instance.UITutorial(0.05f);

            //Time.timeScale = 0f;
            PlayerController.instance.footstepFast.Stop();
            PlayerController.instance.footstepSlow.Stop();




            /* AudioManager.instance.PlaySFX(0);
            AudioManager.instance.PlaySFX(9); */
        }
       
    }


   

}




