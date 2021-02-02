using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnCount : MonoBehaviour
{
    public int countRequired;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
}
    private void LateUpdate()
    {
        print(countRequired);
        print(player.GetComponent<LvlCollectorPickup>().curCount);
        if (countRequired <= player.GetComponent<LvlCollectorPickup>().curCount)
        {
            Destroy(transform.gameObject);
        }
    }
}
