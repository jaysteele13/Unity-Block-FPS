using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public float invincibleLength = 1f;
    private float invincibleCounter;

    public int maximumHealth, currentHealth;


    private void Awake()
    {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;

        UIController.instance.healthSlider.maxValue = maximumHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincibleCounter <= 0 && !GameManager.instance.levelEnding)
        {
            currentHealth -= damageAmount;
            AudioManager.instance.PlaySFX(7);

            UIController.instance.ShowDamage();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;

                GameManager.instance.PlayerDied();

                AudioManager.instance.StopBGM();
                AudioManager.instance.PlaySFX(6);
                AudioManager.instance.StopSFX(7);
            }

            invincibleCounter = invincibleLength;

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maximumHealth;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
     UIController.instance.healthSlider.value = currentHealth;
     UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maximumHealth;
    }
}
