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

    [SerializeField]
    GameObject NextPatrolPoint;

    private void Start()
    {
        OwnCollider = this.GetComponent<SphereCollider>();
        CurTimer = ReactivateTimer;

        if(NextPatrolPoint == null)
        {
            Debug.Log("No NextPatrolPoint set for " + this.gameObject.name);
        }


        this.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OwnCollider.gameObject.SetActive(false);
            CurTimer = ReactivateTimer;
        }
    }

    private void Update()
    {
        if (!OwnCollider.gameObject.activeInHierarchy)
        {
            CurTimer -= Time.deltaTime;
        }
        else
        {
            if(CurTimer <= 0.1f)
            {
                OwnCollider.gameObject.SetActive(true);
            }
        }

    }

    public GameObject GiveNewPatrolPoint()
    {
        return NextPatrolPoint;
    }
}
