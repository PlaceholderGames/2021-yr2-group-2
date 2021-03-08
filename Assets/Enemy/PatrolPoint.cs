using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PatrolPoint : MonoBehaviour
{
    SphereCollider OwnCollider;
    float ReactivateTimer = 2.0f;

    [SerializeField]
    float CurTimer = 0.0f;

    [Tooltip("Next Patrol Point in list")]
    [SerializeField]
    GameObject NextPatrolPoint = null;

    private void Start()
    {
        //Set collider to the own collider
        OwnCollider = this.GetComponent<SphereCollider>();

        //Set cur timer to be the preset timer amount
        CurTimer = ReactivateTimer;

        //Check if there is no next patrol point
        if(NextPatrolPoint == null)
        {
            Debug.Log("No NextPatrolPoint set for " + this.gameObject.name);
        }

        //Disable mesh to appear invisible
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If collided with enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //Deactivate the object (to avoid multiple collision triggers)
            OwnCollider.gameObject.SetActive(false);

            //Reset timer
            CurTimer = ReactivateTimer;
        }
    }

    private void Update()
    {
        //Whilst collider is deactivated count down cooldown
        if (!OwnCollider.gameObject.activeInHierarchy)
        {
            CurTimer -= Time.deltaTime;
        }
        else
        {
            //Once timer is over
            if(CurTimer <= 0.1f)
            {
                //Reactivate collider
                OwnCollider.gameObject.SetActive(true);
            }
        }

    }

    //Give the new control point
    public GameObject GiveNewPatrolPoint()
    {
        return NextPatrolPoint;
    }
}
