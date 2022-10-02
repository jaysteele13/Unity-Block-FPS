using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private Vector3 targetPoint, startPoint;

    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

    private bool wasShot;
    

    private void Start()
    {
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;
        if(!chasing)
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase) // distance between enemy and player less than distance, then chase
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots; //waiting before shot
            }

            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint; // returns to start
                }
            }

            if(agent.remainingDistance < .25)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            if (PlayerController.instance.gameObject.activeInHierarchy)
            {



                /* transform.LookAt(targetPoint);
                theRB.velocity = transform.forward * moveSpeed; */

                //chasing
                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    agent.destination = targetPoint; //tracking player movements
                }
                else
                {
                    agent.destination = transform.position;
                }


                if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
                {
                    if(!wasShot)
                    { 
                        chasing = false;
                        chaseCounter = keepChasingTime;

                    }
                   
                }
                else
                {
                    wasShot = false;
                }

                //waiting to shoot!

                if (shotWaitCounter > 0)
                {
                    shotWaitCounter -= Time.deltaTime;

                    if (shotWaitCounter <= 0)
                    {
                        shootTimeCounter = timeToShoot;
                    }

                    anim.SetBool("isMoving", true); // moving and looking for player 
                }
                else
                {

                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;
                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, .4f, 0f));

                            //check angle of player
                            Vector3 targetDirection = PlayerController.instance.transform.position - transform.position; // minusing position find angle amount
                            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 35f) //signed angle makes it angle, abs -30 / 30 works
                            {
                                anim.SetTrigger("fireShot");
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }



                        }
                        agent.destination = transform.position;
                    }



                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }


                    anim.SetBool("isMoving", false); // as this is where we stop to shoot
                }
            }

           
        }


    }

    public void GetShot()
    {
        wasShot = true;

        chasing = true;

        
    }
}
