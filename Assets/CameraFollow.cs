using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    //public float closeSmoothSpeed = 0.5f;              //Camera update speed when close up
    //public float farSmoothSpeed = 2.0f;                //Camera update speed when far away

    float smoothSpeed;
    public Vector3 offset;
    public int cameraRange;

    void FixedUpdate()
    {
        smoothSpeed = (Vector3.Distance(target.transform.position, transform.position))/10;

        // //If target of camera is beyond a certain distance change the update speed to fast updating
        //if (Vector3.Distance(target.transform.position, transform.position) > cameraRange)
        // {
        //     smoothSpeed = farSmoothSpeed;
        // }
        //else //Otherwise update it at close smooth speed
        // {
        //     smoothSpeed = closeSmoothSpeed;
        // }

       Vector3 desiredPositon = target.position + offset;
       Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPositon, smoothSpeed * Time.deltaTime);
       transform.position = smoothedPosition;

       transform.LookAt(target);
    }
}
