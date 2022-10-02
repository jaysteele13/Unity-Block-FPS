using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealthController : MonoBehaviour
{

    public static EnemyHealthController instance;
    public int currentHealth, maxHealth = 5;

    public GameObject healthBarUI;
    public Slider healthSlider;

    public bool isEnemyWalking, isEnemyTurret; // to benefit tracking hp bars

    public EnemyController theEC;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        if (isEnemyTurret)
        {
            healthBarUI.transform.rotation = Turret.instance.gun.transform.rotation;
        }
    }
    /*float CalculateHealth()
    {
        return currentHealth / maxHealth; 
    } */

    public void DamageEnemy(int damageAmount)
    {
        
        currentHealth -= damageAmount;

        healthSlider.value = currentHealth;
        healthBarUI.SetActive(true);

        if(theEC != null)
        {
            theEC.GetShot();
        }

        

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(2); 
            
        }

    }
}
