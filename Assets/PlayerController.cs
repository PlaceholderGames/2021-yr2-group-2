using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : Entity
{
    [Header("GameInfo")]

    Healthbar Healthbar;
    public Animator anim;
    [SerializeField]
    FlashImage _flashImage = null;

    [Header("Default Character Info")]

    [Tooltip("Defines the standard materials for the character")]
    public Material[] standardMaterial;

    [Header("Character Info")]

    [Range(1, 300)]
    [Tooltip("Defines maximum health of the character")]
    public int maxHealth = 100;

    [Range(1, 300)]
    [Tooltip("Defines current health of the character")]
    public int currentHealth;


    [Tooltip("Defines which progress bar is for the ghost power")]
    ProgressBar PBGhost;

    Canvas canvas;
    public GameObject pauseMenu;
    public GameObject gameOver;


    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        pauseMenu = GameObject.Find("PauseMenu");
        gameOver = GameObject.Find("GameOverMenu");


        //Find the healthbar
        GameObject obj = GameObject.Find("Healthbar");
        Healthbar = obj.GetComponent<Healthbar>();

        MovementController = GetComponentInParent<Movement>();

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

        obj = GameObject.Find("TimeUI");
        

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
        MovementController.moveSpeed = MovementController.baseMoveSpeed;
        PowerGhost.ghostTimer = PowerGhost.ghostTimerMax;

        pauseMenu.SetActive(false);
        gameOver.SetActive(false);

    }
    
    // Update is called once per frame
    void Update()
    {  
        if (!Paused)
        {
            MovementController.HandleMovement(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (PowerTime != null)
            {
                PowerTime.HandleTimePower(PowerTime.TimePowerActive, Input.GetButtonDown("Time"));
            }

            if (PowerGhost != null)
            {
                PowerGhost.HandleGhostPower(PowerGhost.ghostPowerActive, Input.GetButtonDown("Ghost"));
            }


            //Handle Animation
            anim.SetBool("isGrounded", !MovementController.canJump);
            anim.SetFloat("Speed", Mathf.Abs(MovementController.moveDirection.x + MovementController.moveDirection.z));


            //print("Speed = " + Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
            anim.SetBool("isGrounded", MovementController.canJump);
            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
            PBGhost.UpdateCurrent(PowerGhost.ghostTimer);


            if (Input.GetButtonDown("Pause"))
            {
                pauseMenu.SetActive(true);
            }

            if (currentHealth <= 0)
            {
                gameOver.SetActive(true);
            }
        }

        if (pauseMenu != null && gameOver != null)
        {
            Paused = pauseMenu.activeInHierarchy || gameOver.activeInHierarchy;
        }
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
