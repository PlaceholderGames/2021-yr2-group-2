using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Camera))]
public class CameraSeize : MonoBehaviour
{
    public Camera OwnCam;
    public Camera main;
    public CameraFollow cm;
    public bool MakeMeMain;
    public bool ForceMain;

    private void Start()
    {
        OwnCam = this.GetComponent<Camera>();
        main = Camera.main;
        cm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        OwnCam.enabled = false;
    }

    private void Update()
    {
        if(MakeMeMain)
        {
            MakeOwnMain();
            MakeMeMain = false;
        }

        if(ForceMain)
        {
            RevertToMain();
            ForceMain = false;
        }
   
    }

    void MakeOwnMain()
    {

        if (cm.pcControlled)
        {
            main.enabled = false;
            cm.pcControlled = false;
            cm.enabled = false;
        }
        else
        {
            Camera.current.enabled = false;
        }

        OwnCam.enabled = true;
    }

    void RevertToMain()
    {
        main.enabled = true;
        cm.pcControlled = true;
        cm.enabled = true;
        OwnCam.enabled = false;
    }
}
