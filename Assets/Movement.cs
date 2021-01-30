using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Movement : MonoBehaviour
{
    [Range(0, 50)]
    [Tooltip("Define the base move speed of the character")]
    public float baseMoveSpeed = 22;

    public Vector3 moveDirection;

    [Range(0, 50)]
    [Tooltip("Defines the move speed of the character")]
    public float moveSpeed;

    [Tooltip("Defines if the character can jump")]
    public bool canJump = true;

    [Range(5, 20)]
    [Tooltip("Defines the force of jumps of the character")]
    public float jumpForce = 10;

    [Range(0, 5)]
    [Tooltip("Defines to what scale gravity effects the character")]
    public float gravityScale = 2;

    public Entity ControlledEntity;

    private void Start()
    {
        ControlledEntity = GetComponentInParent<Entity>();
    }

    public void HandleMovement(float Vertical, float Horizontal)
    {
        //Store the y move direction for usage later
        float yStore = moveDirection.y;

        //Adjust movement based on facing
        moveDirection = (transform.forward * Vertical) +
            (transform.right * Horizontal);

        //Normalise vector to make sure moving diagonally doesn't double speed
        moveDirection = moveDirection.normalized * moveSpeed;

        //Reapply yStore as moveDirection.y (as would be broken via normalisation)
        moveDirection.y = yStore;

        //Check if on ground
        HandleJump(canJump, Input.GetButtonDown("Jump"));

        //Move in the x taking into account gravity scale and gravity
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

        //Move in the directions built in regards to delta time (rather than set by frame rate)
        ControlledEntity.cController.Move(moveDirection * Time.deltaTime);

        //When player is grounded
        if (ControlledEntity.cController.isGrounded)
        {
            //Renable ability to jump
            canJump = true;

            //So gravity build-up doesnt occur
            moveDirection.y = 0;


        }
    }

    private void HandleJump(bool CanJump, bool IsTriggered)
    {
        if (CanJump)
        {
            //If jumping add jump force to moveDirection
            if (IsTriggered)
            {
                moveDirection.y = jumpForce;
                //StaminaBar.instance.UseStamina(20);//this reduces stamina bar
                canJump = false;
                GetComponent<AudioSource>().Play();
            }
        }
    }

}
