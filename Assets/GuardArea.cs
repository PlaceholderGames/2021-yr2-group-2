using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardArea : MonoBehaviour
{
    GameObject Tresspasser = null;

    public bool TresspasserDetected = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            TresspasserDetected = true;
            Tresspasser = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            TresspasserDetected = false;
            Tresspasser = null;
        }
    }

    public bool Alerted()
    {
        return Tresspasser != null;
    }

    public GameObject GetTresspasser()
    {
        return Tresspasser;
    }
}
