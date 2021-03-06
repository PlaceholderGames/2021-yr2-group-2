using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [Header("General Info")]
    [Tooltip("Enables teleportation on contact")]
    [SerializeField]
    bool allowTeleport = true;

    [Tooltip("If false teleports between levels")]
    [SerializeField]
    bool internalTeleport = true;

    Coroutine delay;

    [Header("External Info")]
    [Tooltip("The level chosen to teleport to if external")]
    [SerializeField]
    private string Level = null;

    [Header("Internal Info")]
    [Tooltip("Linked teleporter if internal")]
    [SerializeField]
    Teleporter teleportPosition = null;

    //Delay to stop teleporting rapidly between teleporters
    public void timedelay()
    {
        allowTeleport = false;
        StartCoroutine(teleportCD());
    }


    //Trigger teleport
    private void OnTriggerEnter(Collider Col)
    {
        if (allowTeleport == true)
        {
            //If internal begin delay timer and teleport character
            if (internalTeleport)
            {
                teleportPosition.timedelay();
                Col.GetComponent<CharacterController>().enabled = false;
                Col.transform.SetPositionAndRotation(teleportPosition.transform.position, teleportPosition.transform.rotation);
                Col.GetComponent<CharacterController>().enabled = true;
            }
            //If non internal and level chosen - load scene
            else if (Level != null)
            {
                Debug.Log(Level + " loaded via teleporter");
                SceneManager.LoadScene(Level);
            }
            else
            {
                Debug.LogError(this + "Error: No Level chosen for external portal");
            }

        }
    }

    public IEnumerator teleportCD()
    {
        allowTeleport = false;
        yield return new WaitForSeconds(3);
        allowTeleport = true;
    }

}
