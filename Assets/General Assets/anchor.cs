using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
             other.gameObject.tag == "Enemy")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
                other.gameObject.tag == "Enemy")
        {
            other.transform.parent = null;
        }

    }
}
