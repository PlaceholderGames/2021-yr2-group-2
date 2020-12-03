using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("GameInfo")]
    CharacterController cController;
    Healthbar Healthbar;
    public Animator anim;
    [SerializeField]
    FlashImage _flashImage = null;

    [Header("Default Character Info")]
    [Range(0, 50)]
    [Tooltip("Define the base move speed of the character")]
    public float baseMoveSpeed = 22;

    [Tooltip("Defines the standard materials for the character")]
    public Material[] standardMaterial;

    [Header("Character Info")]

    private Vector3 moveDirection;

    [Range(0, 50)]
    [Tooltip("Defines the move speed of the character")]
    public float moveSpeed;

    [Range(5, 20)]
    [Tooltip("Defines the force of jumps of the character")]
    public float jumpForce = 10;

    [Range(0, 5)]
    [Tooltip("Defines to what scale gravity effects the character")]
    public float gravityScale = 2;

    [Range(1, 300)]
    [Tooltip("Defines maximum health of the character")]
    public int maxHealth = 100;

    [Range(1, 300)]
    [Tooltip("Defines current health of the character")]
    public int currentHealth;

    [Tooltip("Defines if the character can jump")]
    public bool canJump = true;



    [Tooltip("Defines which progress bar is for the ghost power")]
    ProgressBar PBGhost;


    public GhostPower PowerGhost;
    public TimePower PowerTime;

    // Start is called before the first frame update
    void Start()
    {

        //Find the healthbar
        GameObject obj = GameObject.Find("Healthbar");
        Healthbar = obj.GetComponent<Healthbar>();
        
        //Error check
        if (obj == null)
        {
            print("Could not find Healthbar for PlayerControlller");
        }

        //Find the Flash Image
        obj = GameObject.Find("FlashImage");
        _flashImage = obj.GetComponent<FlashImage>();

        //Error check
        if (obj == null)
        {
            print("Could not find FlashImage for PlayerControlller");
        }

        //Find Ghost UI
        obj = GameObject.Find("GhostUI");
        PBGhost = obj.GetComponent<ProgressBar>();

        if (obj == null)
        {
            print("Could not find GhostUI for PlayerController");
        }
        else
        {
            PBGhost.maximum = (int)PowerGhost.ghostTimerMax;
            PBGhost.minimum = 0;
        }


        cController = GetComponent<CharacterController>();
        Healthbar.slider = Healthbar.gameObject.GetComponent<Slider>(); //initiate healthbar using variables from slider
        currentHealth = maxHealth;                                      //start game with max health
        Healthbar.SetMaxHealth(maxHealth);                              //max health for player set to 100, so max health is 100


        //Use defaults to set info
        moveSpeed = baseMoveSpeed;
        PowerGhost.ghostTimer = PowerGhost.ghostTimerMax;

    }

    // Update is called once per frame
    void Update()
    {

        //Store the y move direction for usage later
        float yStore = moveDirection.y;

        //Adjust movement based on facing
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) +
            (transform.right * Input.GetAxis("Horizontal"));

        if (PowerTime != null)
        {
            PowerTime.HandleTimePower(PowerTime.TimePowerActive, Input.GetButtonDown("Time"));
        }

        //Normalise vector to make sure moving diagonally doesn't double speed
        moveDirection = moveDirection.normalized * moveSpeed;

        //Reapply yStore as moveDirection.y (as would be broken via normalisation)
        moveDirection.y = yStore;

        PowerGhost.HandleGhostPower(PowerGhost.ghostPowerActive, Input.GetButtonDown("Ghost"));

        //Check if on ground
        if (canJump)
        {
            //If jumping add jump force to moveDirection
            if (Input.GetButtonDown("Jump") && StaminaBar.instance.currentStamina >= 20)
            {
                moveDirection.y = jumpForce;
                //StaminaBar.instance.UseStamina(20);//this reduces stamina bar
                canJump = false;
            }
        }

        //Move in the x taking into account gravity scale and gravity
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

        //Move in the directions built in regards to delta time (rather than set by frame rate)
        cController.Move(moveDirection * Time.deltaTime);

        //When player is grounded
        if (cController.isGrounded)
        {
            //Renable ability to jump
            canJump = true;

            //So gravity build-up doesnt occur
            moveDirection.y = 0;
        }

        //Handle Animation
        anim.SetBool("isGrounded", !canJump);
        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x + moveDirection.z));


        //print("Speed = " + Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
        anim.SetBool("isGrounded", canJump);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        PBGhost.UpdateCurrent(PowerGhost.ghostTimer);
        
    }

    public void TakeDamage(int damage) // Take damage code
    {
        if (currentHealth >= 0)
        {
            Healthbar.instance.TakeDamage();
            currentHealth -= damage;
            Healthbar.SetHealth(currentHealth);
            _flashImage.StartFlash(.25f, .5f, Color.red); //When take damage is called, flash image starts
        }
    }
}
