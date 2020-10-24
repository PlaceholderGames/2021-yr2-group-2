using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter teleportposition;
    public bool allowteleport = true;
    public Coroutine delay;




    public void timedelay()
    {
        allowteleport = false;
        StartCoroutine(teleportCD());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider Col)
    {

        if (allowteleport == true)
        {
            teleportposition.timedelay();
            Debug.Log("SHAAAAAATINGGGGG");
            //Col.transform.SetPositionAndRotation(teleportposition.position, teleportposition.rotation);
            //Col.transform.position = teleportposition.position;
            Col.GetComponent<CharacterController>().enabled = false;
            Col.transform.SetPositionAndRotation(teleportposition.transform.position, teleportposition.transform.rotation);
            Col.GetComponent<CharacterController>().enabled = true;

        }
    }

    public IEnumerator teleportCD()
    {
        allowteleport = false;

        yield return new WaitForSeconds(2);

        allowteleport = true;
    }

}
