using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenDestroyed : MonoBehaviour
{

    public GameObject[] list;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        //Destroy all other versions of this pickup
        //if (list.Length != 0)
        //{
        //    Debug.Log(list.Length);
        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        if (list != null)
        //        {
        //            list[i].SetActive(true);
        //        }
        //    }
        //}
    }
}
