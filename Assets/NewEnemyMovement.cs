using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyMovement : MonoBehaviour
{
    public float LookRadius = 10f;

    Transform target;

    NavMeshAgent agent;

     public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.position += transform.forward * 5f * Time.deltaTime;

    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
