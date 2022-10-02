using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifetime;
    public Rigidbody rb;

    public GameObject impactEffect;

    public int damage = 1, headshotMultiplyer = 2;
    



    public bool damageEnemy, damagePlayer;

    void Update()
    {
        rb.velocity = transform.forward * moveSpeed; //don't need time.deltatime as velocity is physics and not per frame

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject); //whatever game object script is attached to
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
           
            
                other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            
        }

        if (other.gameObject.tag == "Headshot" && damageEnemy)
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * headshotMultiplyer);
            Debug.Log("Critical Hit");
        }


        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Player has been hit at " + transform.position);
            PlayerHealthController.instance.DamagePlayer(damage);
        }

        Destroy(gameObject); //destroys bullet if collides
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation); //move back effect so i doesn't spawn in object
    }
}
