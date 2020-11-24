using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [Range(0, 3)]
    [Tooltip("Power pickup for:\n0 - Double Jump\n1 - Ghost\n2 -UNSET\n3 - UNSET")]
    public int powerType = 0;

    [Range(0, 5)]
    [Tooltip("How long after pick up to destroy self")]
    public float delayDestroy = 1.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            //Decide which power to give
            switch(powerType)
            {
                //Double Jump
                case 0:
                    Debug.Log("Double Jump Power Activated");
                    break;

                //Ghost
                case 1:
                    other.GetComponent<PlayerController>().ghostPowerActive = true;
                    Debug.Log("Ghost Power Activated");
                    break;

                //UNSET
                case 2:
                    Debug.Log("UNSET Power Activated");
                    break;

                //UNSET
                case 3:
                    Debug.Log("UNSET Power Activated");
                    break;

                default:
                    Debug.Log("Invalid Power given");
                    break;
            }

            //Destroy self after a delay
            Destroy(gameObject, delayDestroy);

        }
    }

}
