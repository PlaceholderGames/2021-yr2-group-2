using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cController;

    //Player info
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    //Current direction of movement
    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        cController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //Move in the X and Z in regards to move speed
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        
        //Check if on ground
        if (cController.isGrounded)
        {       
            //If jumping add jump force to moveDirection
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }       
        
        
        //Move in the x taking into account gravity scale and gravity
        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        
        //Move in the directions built in regards to delta time (rather than set by frame rate)
        cController.Move(moveDirection * Time.deltaTime) ;
    }
}
