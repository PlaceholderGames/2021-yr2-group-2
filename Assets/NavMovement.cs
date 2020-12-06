using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{

    [SerializeField]
    GameObject CurrentTarget;

    public  GameObject[] PatrolPoints;

    NavMeshAgent NavAgent;

    public float timemax = 4;
    public int x;
    public float timeCur = 4;

    private void Start()
    {
        NavAgent = this.GetComponent<NavMeshAgent>();
        
        CurrentTarget = PatrolPoints[0];

    }


    private void Update()
    {
        timeCur -= Time.deltaTime;

        PatrolUpdate(timeCur <= 0.1f);

        NavAgent.SetDestination(CurrentTarget.transform.position);
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        PatrolUpdate(collision.gameObject);
    }

    public void PatrolUpdate(bool changeNeeded)
    {
        if(changeNeeded)
        {

            for (int i = 0; i < PatrolPoints.Length; i++)
            {
                if(CurrentTarget == PatrolPoints[i])
                {
                    x = i;
                }
            }

            x++;

            if(x >= PatrolPoints.Length)
            {
                x = 0;
            }

            CurrentTarget = PatrolPoints[x];

            timeCur = timemax;
        }
                
    }

}
