using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
    [SerializeField]
    GameObject CurrentTarget;

    GameObject player;

    [SerializeField]
    PatrolPoint LastOrder = null;

    float tolerance;


    [Range(0, 5)]
    public float speed;


    [SerializeField]
    [Range(0, 20)]
    float delayTime = 1.0f;

    [SerializeField]
    float delayCur;

    Vector3 heading;


    [SerializeField]
    bool automatic = true;

    private void Start()
    {
        if(CurrentTarget == null)
        {
            Debug.Log("No Current Target for " + this.gameObject.name);
        }

        LastOrder = CurrentTarget.GetComponent<PatrolPoint>();
        tolerance = speed * Time.deltaTime;
        delayCur = delayTime;
    }

    private void Update()
    {
        if (transform.position != CurrentTarget.transform.position)
        {
            MovePlatform();
        }
        else
        {
            UpdatePlatform();
        }

    }
    void MovePlatform()
    {
        heading = CurrentTarget.transform.position - transform.position;

        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;

        if(heading.magnitude < tolerance)
        {
            transform.position = CurrentTarget.transform.position;
            delayCur = delayTime;
        }

    }

    void UpdatePlatform()
    {
        if (automatic)
        {
            delayCur -= Time.deltaTime;

            if (delayCur <= 0)
            {
                LastOrder = CurrentTarget.GetComponent<PatrolPoint>();
                CurrentTarget = LastOrder.GiveNewPatrolPoint();
                delayCur = delayTime;
            }
        }
    }

}