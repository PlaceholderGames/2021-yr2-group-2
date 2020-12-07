using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{

    [SerializeField]
    GameObject CurrentTarget;

    [SerializeField]
    bool HasGuardArea = false;

    [SerializeField]
    GameObject AssignedGuardArea;

    GuardArea guardArea;

    NavMeshAgent NavAgent;

    [SerializeField]
    PatrolPoint LastOrder;

    public float timemax = 4;
    public int x;
    public float timeCur = 4;
    public bool TimeBasedPatrol = false;


    private void Start()
    {
        NavAgent = this.GetComponent<NavMeshAgent>();

        if(CurrentTarget == null)
        {
            Debug.Log("No Current Target for " +  this.gameObject.name);
        }


        if (AssignedGuardArea == null && HasGuardArea)
        {
            Debug.Log("No Current Guard Area for " + this.gameObject.name);
        }
        else
        {
            if(AssignedGuardArea.GetComponent<GuardArea>() != null)
            {
                guardArea = AssignedGuardArea.GetComponent<GuardArea>();
            }
            else
            {
                Debug.Log("No Current Guard Area script for " + this.gameObject.name);
            }
        }
    }


    private void Update()
    {
        if(guardArea != null && guardArea.Alerted())
        {
            CurrentTarget = guardArea.GetTresspasser();
        }
        else
        {
            CurrentTarget = LastOrder.GiveNewPatrolPoint();
        }


        if(CurrentTarget != null)
        {
            NavAgent.SetDestination(CurrentTarget.transform.position);       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PatrolPoint")
        {
            if(other.GetComponent<PatrolPoint>() != null)
            {
                LastOrder = other.GetComponent<PatrolPoint>();
            }

            CurrentTarget = LastOrder.GiveNewPatrolPoint();
        }
    }

}
