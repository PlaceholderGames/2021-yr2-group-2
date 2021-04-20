using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlCollectorPickup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tracks current count of picked up Collectables")]
    int curCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().pitch = 1.0f;
        if (other.tag == "Lvl Complete Pickup")
        {
            curCount++;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().pitch = 1.25f;
            GetComponent<AudioSource>().Play();
        }
        else if (other.tag == "PP Ghost")
        {
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }


    }
}
