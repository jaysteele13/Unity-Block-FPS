using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // unique version of script, can only ever have one value
    //pleaser by wallows is erins favourite song! ATM

   

    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 12f;
    public CharacterController characterController;

    private Vector3 moveInput; //storing how much we want player to move

    public Transform camTransform;

    public float mouseSensitivity;
    public bool invertX, invertY;

    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround; //

    //public GameObject bullet;
    public Transform firePoint;

    public Gun activeGun;


    public Animator anim;

    public List<Gun> allGuns = new List<Gun>(); // list of guns to change too always changing
    public List<Gun> unlockableGuns = new List<Gun>();
          
    public int currentGun;

    public Transform adsPoint, gunHolder;
    private Vector3 gunStartPos;
    public float adsSpeedIn = 2f, adsSpeedOut = 25;

    public GameObject muzzleFlash;

    public AudioSource footstepFast, footstepSlow;

    private float bounceAmount;
    private bool isBounce;

    public float maxViewAngle = 60f;

    private void Awake()
    {
        instance = this; //something that hapens before start, this - player object is instance
    }

    private void Start()
    {
        currentGun--;
        SwitchGun();

        gunStartPos = gunHolder.localPosition; // local accesses local position in terms of the active camera point 
    }


    private void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //axis = the default horizontal key of player
            // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            //store y velocity

            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");


            moveInput = horiMove + vertMove;
            moveInput.Normalize(); //going diagonal makes it too fast, normalise does maths for us and fixes bug!

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput *= runSpeed;
            }
            else
            {
                moveInput = moveInput * moveSpeed;
            }


            //gravity
            moveInput.y = yStore;
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (characterController.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }


            canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0; // centre of sphere is where jump is detected, any objects within? more than 0 then canJump is true - float (0.25) is size of sphere!



            //handle jumping

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpPower;
                canDoubleJump = true;

                AudioManager.instance.PlaySFX(8);

                anim.SetBool("onGround", false); //may need to change back (delete and other anim.setfloat(fallSpeed).

            }
            else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpPower;
                
                canDoubleJump = false;

                AudioManager.instance.PlaySFX(8);
            }

            if(isBounce)
            {
                isBounce = false;
                moveInput.y = bounceAmount;

                canDoubleJump = true; 
            }


            characterController.Move(moveInput * Time.deltaTime); //move function tells controller how much player is moving


            //Control Camera Rotation
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity; //raw - no smothing and speed to look

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }

            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);  //rotating only player, Euler converts 4 number to 3
            camTransform.rotation = Quaternion.Euler(camTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f)); //- to stop cam inversion

            //limiting view
            if(camTransform.rotation.eulerAngles.x > maxViewAngle && camTransform.rotation.eulerAngles.x < 180f)
            {
                camTransform.rotation = Quaternion.Euler(maxViewAngle, camTransform.rotation.eulerAngles.y , camTransform.rotation.eulerAngles.z);
            }
            else if(camTransform.rotation.eulerAngles.x > 180 && camTransform.rotation.eulerAngles.x < 360f - maxViewAngle)
            {
                camTransform.rotation = Quaternion.Euler(-maxViewAngle, camTransform.rotation.eulerAngles.y, camTransform.rotation.eulerAngles.z);
            }
            //SHOOTING


            //muzzle flash

            muzzleFlash.SetActive(false); // as updte checks per frame, muzzle is shown for frame then cancelled

            //single shots


            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit; //invisible line to detect crosshairs **
                if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 50f))
                {
                    if (Vector3.Distance(camTransform.position, hit.point) > 2) //fixing bugs if gun is to close to object
                    {
                        firePoint.LookAt(hit.point); //where did the raycast hit against another object
                    }
                }
                else
                {
                    firePoint.LookAt(camTransform.position + (camTransform.forward * 30));// centreish far away
                }

                //Instantiate(bullet, firePoint.position, firePoint.rotation);
                FireShot();
            }
        }

        //repeats shots


        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }




        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }

        //aiming

        

        if(Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(activeGun.zoomAmount);
        }

       if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeedIn * Time.deltaTime);
            
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, adsSpeedOut * Time.deltaTime);
        }
        
       

        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut(); 
        }

        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("onGround", canJump);
        anim.SetFloat("fallSpeed", moveInput.y);

        }
    
    

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {

            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);

            activeGun.fireCounter = activeGun.fireRate;

          
            UIController.instance.ammoText.text = "" + activeGun.currentAmmo;

            muzzleFlash.SetActive(true);
        }

       
    }

    public void SwitchGun()
    {


        activeGun.gameObject.SetActive(false);

        currentGun++;

        if(currentGun >= allGuns.Count)
        {
            currentGun = 0;
            
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);


        
        UIController.instance.ammoText.text = "" + activeGun.currentAmmo;

        firePoint.position = activeGun.firepoint.position; 


    }

    public void AddGun(string gunToAdd) //understand cause it be complicated
    {
        bool gunUnlocked = false;

        if(unlockableGuns.Count > 0)
        {
            for (int i = 0; i < unlockableGuns.Count; i++)
            {
                if(unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;
                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count; 
                }
            }
        }


        if(gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            SwitchGun();
        }
    }

    public void Bounce(float bounceForce)
    {
        bounceAmount = bounceForce;
        isBounce = true;
    }
}
