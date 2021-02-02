using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Entity ETarget;

    public Transform target;        //Current Target
    public Transform oldTarget;     //Stored Old Target
    float smoothSpeed;
    public Vector3 offset;
    public int cameraRange;

    public float rotateSpeed;
    public bool useOffsetValues;
    public bool pcControlled;

    public Transform pivot;         //Pivot Point
    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    bool Paused = false;
    private void Start()
    {
        //Find the Player
        GameObject obj = GameObject.Find("Player");
        target = obj.transform;
        ETarget = obj.GetComponent<Entity>();

        //Error check
        if(obj == null)
        {
            print("Could not find Player for CameraFollow");
        }

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        //Set the pivot = the target transform
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //Make cursor disappear on launch
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Paused = ETarget.Paused;

        if (!Paused)
        {
            if (pcControlled)
            {
                //Get x position of mouse (works for joysticks too) & rotate the target of the camera
                float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
                target.Rotate(0, horizontal, 0);

                //Get y position of mouse (works for joysticks too) & rotate the pivot point
                float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
                pivot.Rotate(-vertical, 0, 0);

                //To allow controller choice for Y inversion
                if (invertY)
                {
                    pivot.Rotate(vertical, 0, 0);
                }
                else
                {
                    pivot.Rotate(-vertical, 0, 0);
                }

                //If pivot is above the max angle & below the 180f threshold
                if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
                {
                    //Set it to the max angle
                    pivot.rotation = Quaternion.Euler(maxViewAngle, 0f, 0f);
                }

                //If the pivot is greater than the 180 threshold and below 360 (-) + the minimum view angle
                if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
                {
                    //Set it to 360 (-) + the minViewAngle
                    pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0f, 0f);
                }

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
            if (transform.position.y < target.position.y)
            {
                //Make the y = the y of the target
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }

            //Look towards the target
            transform.LookAt(target);
        }
    }


    //Set the camera to focus on a new point and transform
    public void NewTarget(Transform newTran)
    {
        oldTarget = target;
        target = newTran;
        pcControlled = false;
    }

   //Return to previous target / pivot
    public void RevertTarget()
    {
        target = oldTarget;
        pcControlled = true;
    }
}
