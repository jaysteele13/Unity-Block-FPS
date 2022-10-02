using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{

    public string mainMenuScene;

    public float timeBetweenShowing = 3f;

    public GameObject textBox, returnButton, lilBitch, sas;

    public Image fadeScreen;
    public float fadeSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(ShowObjectsCo()); 
    }

    // Update is called once per frame
    void Update()
    {
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene); 
    }

    public IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(timeBetweenShowing);

        textBox.SetActive(true);

        yield return new WaitForSeconds(timeBetweenShowing);


        lilBitch.SetActive(true);

        yield return new WaitForSeconds(4f);

        sas.SetActive(true);

        

        yield return new WaitForSeconds(2.5f);

        returnButton.SetActive(true);


    }
}
