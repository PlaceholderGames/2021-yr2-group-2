﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public CharacterController cController;
    public Healthbar Healthbar;
    //flashimage script:
    [SerializeField] FlashImage _flashImage = null;


    //Player info
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;



    //Ghost related variables
    public bool ghostAbilityActive = false;
    public bool canJump = true;
    public bool canGhost = true;
    public bool isGhost = false;
    public float ghostTimer = 3.0f;
    public float ghostTimerMax = 3.0f;
    public float ghostTimerRegenMultipler = 1.0f;
    public Material standardMaterial;
    public Material ghostMaterial;
    ProgressBar ghost;

    //Current direction of movement
    private Vector3 moveDirection;


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
        ghost = obj.GetComponent<ProgressBar>();

        if (obj == null)
        {
            print("Could not find GhostUI for PlayerController");
        }
        else
        {
            ghost.maximum = (int)ghostTimerMax;
            ghost.minimum = 0;
        }


        cController = GetComponent<CharacterController>();
        Healthbar.slider = Healthbar.gameObject.GetComponent<Slider>(); //initiate healthbar using variables from slider
        currentHealth = maxHealth;//start game with max health
        Healthbar.SetMaxHealth(maxHealth);//max health for player set to 100, so max health is 100


    }

    // Update is called once per frame
    void Update()
    {

        //Store the y move direction for usage later
        float yStore = moveDirection.y;

        //Adjust movement based on facing
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) +
            (transform.right * Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.T))
        {
            Time.timeScale = 0.5f;
            moveSpeed = 22;
        }
        else
        {
            Time.timeScale = 1;
            moveSpeed = 11;
        }

        //Normalise vector to make sure moving diagonally doesn't double speed
        moveDirection = moveDirection.normalized * moveSpeed;

        //Reapply yStore as moveDirection.y (as would be broken via normalisation)
        moveDirection.y = yStore;

        if (ghostAbilityActive)
        {
            //If a ghost
            if (isGhost)
            {
                //Turn off collision with passable terrain
                Physics.IgnoreLayerCollision(0, 8);


                //Start counting down the ghost timer
                ghostTimer -= Time.deltaTime;

                //If you ghost timer -ve
                if (ghostTimer <= 0)
                {
                    //Come out of ghost form
                    isGhost = false;

                    //Go back to default look
                    GetComponent<Renderer>().material = standardMaterial;
                }


            }
            else //If not a ghost
            {
                //Restart collision with passable layer
                Physics.IgnoreLayerCollision(0, 8, false);

                //If you press activate ghost power and cooldown > 1
                if (Input.GetButtonDown("Ghost") && ghostTimer > 1)
                {
                    //become a ghost
                    isGhost = true;

                    //Change look into ghosty form
                    GetComponent<Renderer>().material = ghostMaterial;
                }
                else
                {
                    //...increase the ghost time avaliable
                    ghostTimer += ghostTimerRegenMultipler * Time.deltaTime;

                    //within cap
                    if (ghostTimer > ghostTimerMax) ghostTimer = ghostTimerMax;
                }
            }
        }

        //Check if on ground
        if (canJump)
        {
            //If jumping add jump force to moveDirection
            if (Input.GetButtonDown("Jump") && StaminaBar.instance.currentStamina >= 20)
            {
                moveDirection.y = jumpForce;
                TakeDamage(20);//just to test that damage gets inflicted and healthbar works, will be moved onto enemies when possible
                StaminaBar.instance.UseStamina(20);//this reduces stamina bar
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
        print("Can Jump = " + canJump);
        anim.SetBool("isGrounded", canJump);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        ghost.UpdateCurrent(ghostTimer);

        print(cController.isGrounded);

        print(Time.deltaTime);
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
