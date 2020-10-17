using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float smoothSpeed;
    public Vector3 offset;
    public int cameraRange;

    public float rotateSpeed;
    public bool useOffsetValues;
    public bool pcControlled;

    public Transform pivot;

    private void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        //Set the pivot = the target transform
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //Make cursor disappear on launch
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (pcControlled)
        {
            //Get x position of mouse (works for joysticks too) & rotate the target of the camera
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            target.Rotate(0, horizontal, 0);

            //Get y position of mouse (works for joysticks too) & rotate the pivot point
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            pivot.Rotate(-vertical, 0, 0);

            //Move camera based on current rotation & offset
            float desiredYAngle = target.eulerAngles.y;
            float desiredXAngle = pivot.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position = target.position - (rotation * offset);
        }
        else
        {
            //Base the speed of the camera update on distance from target
            smoothSpeed = (Vector3.Distance(target.transform.position, transform.position)) / 10;

            //Offset the desired position
            Vector3 desiredPositon = target.position - offset;

            //Make the small changees to target position based on the desired position and smooth speed
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPositon, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

        }

        //If lower that what the camera is looking at
        if(transform.position.y < target.position.y)
        {
            //Make the y = the y of the target
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        //Look towards the target
        transform.LookAt(target);
    }
}
