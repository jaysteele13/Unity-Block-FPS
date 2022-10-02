using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    public string nextLevel;

    public float waitToEndLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.levelEnding = true;

            PlayerController.instance.footstepFast.Stop();
            PlayerController.instance.footstepSlow.Stop();

            AudioManager.instance.PlayLevelVictory();// play rin
            StartCoroutine(EndLevelCo());
        }
    }


    private IEnumerator EndLevelCo()
    {
        PlayerPrefs.SetString(nextLevel + "_cp", "");

        yield return new WaitForSeconds(waitToEndLevel);

        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", ""); // setting check point to blank
        Debug.Log("Checkpoints Reset");

        SceneManager.LoadScene(nextLevel);

        

    }
    
}
