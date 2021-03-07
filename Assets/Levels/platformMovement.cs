using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{
    [Header("Targetting Info")]

    [Tooltip("Will the platform automatically move between points")]
    [SerializeField]
    private bool automatic = true;

    [Tooltip("Current goal location for platform")]
    [SerializeField]
    private GameObject CurrentTarget = null;

    [Tooltip("Speed of platform movement")]
    [SerializeField]
    [Range(0, 5)]
    private float speed = 1.0f;
    
    [Tooltip("Delay time at location")]
    [SerializeField]
    [Range(0, 20)]
    private float delayTime = 1.0f;

    [Tooltip("Current count for triggered delay")]
    [SerializeField]
    private float delayCur = 1.0f;

    [Tooltip("Previous target for platform")]
    [SerializeField]
    private PatrolPoint LastOrder = null;

    [Tooltip("Acceptable leeway to count as 'at location'")]
    [SerializeField]
    private float tolerance = 0.0f;

    //Current movement direction
    private Vector3 heading;

    [Header("References")]
    [Tooltip("Reference to the player")]
    [SerializeField]
    private GameObject player = null;

    private Vector3 lastPos; //store the platform's position from during the last Update loop

    private void Start()
    {
        //Should there be no target - alert via debug log
        if(CurrentTarget == null)
        {
            Debug.Log(this.gameObject.name + ": has no target");
        }

        //Set the last order as current
        SetLastOrder(GetTarget());
        SetTolerance(speed * Time.deltaTime);
        SetCurDelay(delayTime);
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
        if (player)
        {
            Vector3 dir = (transform.position - lastPos).normalized;
            player.GetComponent<CharacterController>().Move((dir * speed) * Time.deltaTime);
        }
        lastPos = transform.position;
    }
    
    private void OnTriggerStay(Collider collider)
    {
        print("Stay");
        //Capture a ref to player only if the player variable is currently null/unassigned to prevent recapturing if the player tries to move again
        if (collider.gameObject.tag=="Player" & !player)
        {
            player = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        print("exit");
        if (collider.gameObject.tag == " Player" && player)
        {
            player = null;
        }
    }

    //Move Platform to new target
    private void MovePlatform()
    {
        //Work out current heading
        heading = CurrentTarget.transform.position - transform.position;

        //Move based on heading and speed
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        
        //If in tolerable range set to exact location
        if(heading.magnitude < tolerance)
        {
            transform.position = CurrentTarget.transform.position;
            delayCur = delayTime;
        }

    }
   
    //Update self
    private void UpdatePlatform()
    {
        //Should the platform move automatically
        if (automatic)
        {
            //Update timer
            delayCur -= Time.deltaTime;

            //Should timer expire update target
            if (delayCur <= 0)
            {
                LastOrder = GetTarget();
                CurrentTarget = LastOrder.GiveNewPatrolPoint();
                delayCur = delayTime;
            }
        }
    }

    //Set a new target
    public void SetTarget(GameObject target)
    {
        CurrentTarget = target;
    }

    //Set a new target
    public void SetTarget(PatrolPoint target)
    {
        LastOrder = target;
        SetTarget(target.gameObject);
    }

    //Set the last order
    public void SetLastOrder(PatrolPoint target)
    {
        LastOrder = target;
    }

    //Get the current target
    public PatrolPoint GetTarget()
    {
        PatrolPoint tmp = CurrentTarget.GetComponent<PatrolPoint>();

        if (tmp == null)
        {
            Debug.LogError("Target is not patrol point");
        }

        return tmp;
    }

    //Get the last order
    public PatrolPoint GetLastOrder()
    {
        return LastOrder;
    }

    //Set the tolerable leeway for the target
    public void SetTolerance(float value)
    {
        tolerance = value;
    }

    //Set the tolerable leeway for the target
    public void SetTolerance(int value)
    {
        SetTolerance((float)value);
    }

    //Set the current delay on the timer
    public void SetCurDelay(float value)
    {
        delayCur = value;
    }

    //Set the current delay on the timer
    public void SetCurDelay(int value)
    {
        SetCurDelay((float)value);
    }

}