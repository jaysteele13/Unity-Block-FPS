using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{

    public string cpName;
    public static CheckpointController instance;

    //player prefs system - way of storing values behind the scenes - checkpoints

    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)
            {
                PlayerController.instance.transform.position = transform.position;
                Debug.Log("Player starting at " + cpName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", ""); // setting check point to blank
            Debug.Log("Checkpoints Reset");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //setting value test_cp - cp1
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            Debug.Log("Player Hit " + cpName);

            AudioManager.instance.PlaySFX(1); //plays checkpoint sound

        }
    }
}
