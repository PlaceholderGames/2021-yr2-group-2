using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavacode : MonoBehaviour
{


    public List<PlayerController> Playerinlava = new List<PlayerController>();


   
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("lavadamage", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.CompareTag("Player"))

        {

            Playerinlava.Add(Col.gameObject.GetComponent<PlayerController>());
        }
    }

    private void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.CompareTag("Player"))

        {
            Playerinlava.Remove(Col.gameObject.GetComponent<PlayerController>());

        }
    }


    void lavadamage()
    {
        for (int i = 0; i < Playerinlava.Count; i++)
        {
            Playerinlava[i].TakeDamage(1);
        }



    }
}
