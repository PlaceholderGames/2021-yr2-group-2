using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class NewEnemyMovement : MonoBehaviour
{
    //Current location of the target the enemy is aiming for
    Transform target;

    //Check to see if enemy has target
    bool found = false;

    [Tooltip("Reference for the player")]
    [SerializeField]
    GameObject player;

    [Tooltip("Reference for the player")]
    [SerializeField]
    float speed = 5.0f;

    void Start()
    {
        //allows ai to find player and recognise where the player is 
        GameObject obj = GameObject.Find("Player");

        //error showing if enemy cannot find player
        if (obj == null)
        {
            print("Error enemy finding player");
        }
        else
        {
            target = obj.transform;
        }
    }
    //movement of the character
    void Update()
    {
        //Whilst the enemy has found a target
        if (found == true)
        {
            //Look towards the target 
            transform.LookAt(target.transform);

            //Move towards the position in scale to speed
            transform.position += transform.forward * speed * Time.deltaTime;
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