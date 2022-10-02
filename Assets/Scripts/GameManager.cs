using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitAfterDeath = 2f;

    public string victoryScene;

    public float waitForVictory = 1f;

    public float timeToWaitUnpause;

    [HideInInspector]
    public bool levelEnding;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

        
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
            
        }

        /*if (EnemyHealthController.instance.currentHealth <= 0)
        {
            PlayerWon();

        }
        */


        //making tutorial pause work

        if (WalkThroughTutorial.instance.tutorialTriggered)
        {
            

            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("My code is working");


                WalkThroughTutorial.instance.blockade.SetActive(false); 
                UIController.instance.tutorialFadeSpeed = 2f;


                PlayerController.instance.footstepFast.Stop();
                PlayerController.instance.footstepSlow.Stop();

                
                WalkThroughTutorial.instance.tutorialTriggered = false;
                WalkThroughTutorial.instance.gameObject.SetActive(false);
                Time.timeScale = 0f;

                Time.timeScale = 1f; 


            }
           
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
        
    }

    //player won coroutine
    public void PlayerWon()
    {
        StartCoroutine(PlayerWonCo());
    }

    public IEnumerator PlayerWonCo()
    {
        yield return new WaitForSeconds(waitForVictory);

        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(victoryScene);
    }

    //player died coroutine
    public IEnumerator PlayerDiedCo() // wait time aside of other functions
    {
        yield return new WaitForSeconds(waitAfterDeath);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {

       
            if (UIController.instance.pauseScreen.activeInHierarchy)
            {
                UIController.instance.pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; //freedom of mouse
                Cursor.visible = false;

            Time.timeScale = 1f; 

            PlayerController.instance.footstepFast.Play();
            PlayerController.instance.footstepSlow.Play();

        }
            else if(!UIController.instance.pauseScreen.activeInHierarchy)
            {

                

                UIController.instance.pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;



            Time.timeScale = 0f;

            PlayerController.instance.footstepFast.Stop();
            PlayerController.instance.footstepSlow.Stop();


        }
        
     
       
      
        
        
    }
   

}
