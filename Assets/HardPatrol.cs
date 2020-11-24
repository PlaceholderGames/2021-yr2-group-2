using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPatrol : MonoBehaviour
{

    public GameObject point0, point1;

    public bool patrolling;
    Transform x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolling)
        {
            //if (transform == point0)
            //{( 
            x.position.Set((float)point0.transform.position.x + 1, (float)point0.transform.position.y, (float)point0.transform.position.z);
            transform.SetParent(x);
            //}
            //else if(transform == point1)
            //{
            //    transform.SetParent(point0);
            //}

        }
    }


}
