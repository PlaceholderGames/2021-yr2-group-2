using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;

public class CameraSeize : MonoBehaviour
{
    public CameraFollow cam;


    private void Start()
    {
        //Find the Main Camera
        GameObject obj = GameObject.Find("Main Camera");
        cam = obj.GetComponent<CameraFollow>();

        //Error message
        if(obj == null)
        {
            print("Could not find camerafollow for Camera");
        }

    }

    //Become the focus of the camera
    void TakeCameraControl()
    {
        cam.NewTarget(this.transform);
    }

    //Make the previous focus the new focus of the camear0
    void RevertCameraControl()
    {
        cam.RevertTarget();
    }


    //For testing obtain ownership of the camera when the player collides
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            TakeCameraControl();
        }
    }
    
    //For testing revert camera control when the player leaves
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            RevertCameraControl();
        }
    }
}
