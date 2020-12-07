using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{


    public GameObject Player;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OMG U SMELL" + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {

            GetComponent<Animation>()["DiskLeftRight"].speed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GetComponent<Animation>()["DiskLeftRight"].speed = 1;
        }
    }



}
