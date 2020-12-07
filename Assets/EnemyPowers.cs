using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowers : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<GhostPower>() != null)
        {
            Debug.Log("Has Ghost Power");
            GetComponentInParent<GhostPower>().HandleGhostPower();
        }
        
    }
}
