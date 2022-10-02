using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider, ammoSlider;
    public Text healthText, ammoText;

    public Image damageEffect, bounceEffect, levelCompleteEffect, tutorialShootHimEffect, fistEffect;
    public Text bounceText, levelText, tutorialShootText, fistText1, fistText2, fistText3;                                 
    public float damageAlpha = .2f, bounceAlpha = 1f, damageFadeSpeed = 2f, bounceFadeSpeed, tutorialFadeSpeed, levelFadeSpeed = 1.5f, fistFadeSpeed, fistAlpha = 1f;

    public GameObject pauseScreen; 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //damage dealt
        if(damageEffect.color.a != 0)
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }

        //bounce effect bootry
        if(bounceEffect.color.a != 0)
        {
            bounceEffect.color = new Color(bounceEffect.color.r, bounceEffect.color.g, bounceEffect.color.b, Mathf.MoveTowards(bounceEffect.color.a, 0f, bounceFadeSpeed * Time.deltaTime));
        }

        if(bounceText.color.a != 0)
        {
            bounceText.color = new Color(bounceText.color.r, bounceText.color.g, bounceText.color.b, Mathf.MoveTowards(bounceText.color.a, 0f, bounceFadeSpeed * Time.deltaTime));
        }

        //fist effect

        if (fistEffect.color.a != 0)
        {
            fistEffect.color = new Color(fistEffect.color.r, fistEffect.color.g, fistEffect.color.b, Mathf.MoveTowards(fistEffect.color.a, 0f, fistFadeSpeed * Time.deltaTime));
        }

        if (fistText1.color.a != 0)
        {
            fistText1.color = new Color(fistText1.color.r, fistText1.color.g, fistText1.color.b, Mathf.MoveTowards(fistText1.color.a, 0f, fistFadeSpeed * Time.deltaTime));
        }

        if (fistText2.color.a != 0)
        {
            fistText2.color = new Color(fistText2.color.r, fistText2.color.g, fistText2.color.b, Mathf.MoveTowards(fistText2.color.a, 0f, fistFadeSpeed * Time.deltaTime));
        }

        if (fistText3.color.a != 0)
        {
            fistText3.color = new Color(fistText3.color.r, fistText3.color.g, fistText3.color.b, Mathf.MoveTowards(fistText3.color.a, 0f, .08f * Time.deltaTime));
        }


        //tutorial shoot him
        if (tutorialShootHimEffect.color.a != 0)
        {
            tutorialShootHimEffect.color = new Color(tutorialShootHimEffect.color.r, tutorialShootHimEffect.color.g, tutorialShootHimEffect.color.b, Mathf.MoveTowards(tutorialShootHimEffect.color.a, 0f, tutorialFadeSpeed * Time.deltaTime));
        }

        if (tutorialShootText.color.a != 0)
        {
            tutorialShootText.color = new Color(tutorialShootText.color.r, tutorialShootText.color.g, tutorialShootText.color.b, Mathf.MoveTowards(tutorialShootText.color.a, 0f, tutorialFadeSpeed * Time.deltaTime));
        }

        // when level ends UI
        if (!GameManager.instance.levelEnding)
        {
            levelCompleteEffect.color = new Color(levelCompleteEffect.color.r, levelCompleteEffect.color.g, levelCompleteEffect.color.b, Mathf.MoveTowards(levelCompleteEffect.color.a, 0f, levelFadeSpeed * Time.deltaTime));
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, Mathf.MoveTowards(levelText.color.a, 0f, levelFadeSpeed * Time.deltaTime));
        }
        else
        {
            levelCompleteEffect.color = new Color(levelCompleteEffect.color.r, levelCompleteEffect.color.g, levelCompleteEffect.color.b, Mathf.MoveTowards(levelCompleteEffect.color.a, 1f, levelFadeSpeed * Time.deltaTime));
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, Mathf.MoveTowards(levelText.color.a, 1f, levelFadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlpha);
    }

    public void ShowBounce()
    {
        bounceEffect.color = new Color(bounceEffect.color.r, bounceEffect.color.g, bounceEffect.color.b, bounceAlpha);
        bounceText.color = new Color(bounceText.color.r, bounceText.color.g, bounceText.color.b, bounceAlpha);
    }

    public void ShowFist()
    {
        fistEffect.color = new Color(fistEffect.color.r, fistEffect.color.g, fistEffect.color.b, fistAlpha);
        fistText1.color = new Color(fistText1.color.r, fistText1.color.g, fistText1.color.b, fistAlpha);
        fistText2.color = new Color(fistText2.color.r, fistText2.color.g, fistText2.color.b, fistAlpha);
        fistText3.color = new Color(fistText3.color.r, fistText3.color.g, fistText3.color.b, fistAlpha);
    }

    public void UITutorial(float fadeSpeed)
    {
        //shoot em


        if (tutorialShootHimEffect.color.a != 0)
        {
            tutorialShootHimEffect.color = new Color(tutorialShootHimEffect.color.r, tutorialShootHimEffect.color.g, tutorialShootHimEffect.color.b, Mathf.MoveTowards(tutorialShootHimEffect.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        if (tutorialShootText.color.a != 0)
        {
            tutorialShootText.color = new Color(tutorialShootText.color.r, tutorialShootText.color.g, tutorialShootText.color.b, Mathf.MoveTowards(tutorialShootText.color.a, 0f, fadeSpeed * Time.deltaTime));
        }

        tutorialShootHimEffect.color = new Color(tutorialShootHimEffect.color.r, tutorialShootHimEffect.color.g, tutorialShootHimEffect.color.b, bounceAlpha);
        tutorialShootText.color = new Color(tutorialShootText.color.r, tutorialShootText.color.g, tutorialShootText.color.b, bounceAlpha);

    }



}
