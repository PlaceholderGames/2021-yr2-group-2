using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyMovement : MonoBehaviour
{
    public float LookRadius = 10f;

    Transform target;

    public bool found = false;

    NavMeshAgent agent;

    public GameObject player;

    void Start()
    {
        //allows ai to find player and recognise where the player is 
        GameObject obj = GameObject.Find("Player");
        target = obj.transform;
        //error showing if enemy cannot find player
        if(obj = null)
        {
            print("Error enemy finding player");
        }
    }
    //movement of the character
    void Update()
    {
        if (found == true)
        {
            transform.LookAt(target.transform);
            transform.position += transform.forward * 5f * Time.deltaTime;
        }
    }
//allowing trigger to recognise player
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            found = true;
        }
    }
    

}