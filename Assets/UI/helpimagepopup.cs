using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class helpimagepopup : MonoBehaviour
{

    public GameObject helpimage;

    public float offtimer;

    // Start is called before the first frame update
    void Start()
    {
        helpimage.SetActive(true);
        Invoke("hide", offtimer);
    }

    // Update is called once per frame
    void hide()
    {

        helpimage.SetActive(false);


    }
}
