using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 25;

    public bool damageEnemy, damagePlayer;

    public float forceOfObject = 50f, radiusOfExplosion = 5f;

     

    private void Update()
    {
        ObjectWeight[] cubesAffected;
        cubesAffected = GameObject.FindObjectsOfType<ObjectWeight>(); 

        foreach (ObjectWeight objectLook in cubesAffected)
        {
            objectLook.CheckRocket(transform.position, radiusOfExplosion, forceOfObject);


        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {


            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);

        }



        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            //Debug.Log("Player has been hit at " + transform.position);
            PlayerHealthController.instance.DamagePlayer(damage);

        }

       
       
    }
}
