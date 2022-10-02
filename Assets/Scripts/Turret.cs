using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public static Turret instance;

    public GameObject bullet;

    public float rangeToTargetPlayer, timeBetweenShots = .5f, distanceToDelete, waitBeforeDeath = 2f;
    private float shotCounter;

    public Transform gun, firepoint1, firepoint2;

    public float rotateSpeed = 6f;

    public Rigidbody turretRigid;

    public bool launchAllowed;

    public ParticleSystem explosionEffect;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = timeBetweenShots; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer) //how far play is from object
        {
            gun.LookAt(PlayerController.instance.transform.position + new Vector3(0f, .35f, 0f)); //giving vertocal axis more leeway

            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                Instantiate(bullet, firepoint1.position, firepoint1.rotation);
                
                shotCounter = timeBetweenShots;

                Instantiate(bullet, firepoint2.position, firepoint2.rotation);
            }  

        }
        else
        {
            shotCounter = timeBetweenShots;

            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10, 0f), rotateSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rocket")
        {

            if (turretRigid.isKinematic)
            {
                turretRigid.isKinematic = false;
                //launchAllowed = true;
            }
        
            
                StartCoroutine(TurretWaitDissapearCo());
            

        }
    }


    public IEnumerator TurretWaitDissapearCo()
    {

        turretRigid.freezeRotation = false;
        yield return new WaitForSeconds(waitBeforeDeath);


        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToDelete && launchAllowed)
        {
            

            AudioManager.instance.StopSFX(2); 
            AudioManager.instance.PlaySFX(2);

            
            // AudioManager.instance.PlaySFX(11); audio shot be implosion sound effect
            Destroy(gameObject);
            explosionEffect.transform.position = transform.position;
            Instantiate(explosionEffect);

        }
        else
        {
            turretRigid.MoveRotation(Quaternion.Euler(0f, 0f, 0f)); 
            turretRigid.freezeRotation = true; 
        }

       
    }
}
