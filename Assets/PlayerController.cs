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
        //Store the y move direction for usage later
        float yStore = moveDirection.y;

        //Adjust movement based on facing
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + 
            (transform.right * Input.GetAxis("Horizontal"));

        //Normalise vector to make sure moving diagonally doesn't double speed
        moveDirection = moveDirection.normalized * moveSpeed;

        //Reapply yStore as moveDirection.y (as would be broken via normalisation)
        moveDirection.y = yStore;
        
        //Check if on ground
        if (cController.isGrounded)
        {
            //So gravity build-up doesnt occur
            moveDirection.y = 0f;

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
