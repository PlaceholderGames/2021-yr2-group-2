using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_to_Swamp : MonoBehaviour
{
    [SerializeField]
    string Level = null;


    private void OnTriggerEnter(Collider other)
    {
        if (Level != null)
        {
            Application.LoadLevel(Level);
        }

        else
        {
            Debug.Log("Level not set");
        }
    }


}
