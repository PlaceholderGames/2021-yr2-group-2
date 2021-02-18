using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport_to_Swamp : MonoBehaviour
{
    [Tooltip("The level chosen to teleport to")]
    [SerializeField]
    private string Level = null;


    private void OnTriggerEnter(Collider other)
    {
        if (Level != null)
        {
            SceneManager.LoadScene(Level);
        }

        //Should no level be selected send error message to console
        else
        {
            Debug.LogError("Level not set");
        }
    }


}
