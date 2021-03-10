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
        if (other.tag == "Lvl Complete Pickup")
        {
            curCount++;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
