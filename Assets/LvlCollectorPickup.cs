using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlCollectorPickup : MonoBehaviour
{
    [Tooltip("Tracks current count of picked up Collectables")]
    public int curCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lvl Complete Pickup")
        {
            curCount++;
            Debug.Log("Lvl Pickup Picked Up");
            Destroy(other.gameObject);
        }
    }
}
