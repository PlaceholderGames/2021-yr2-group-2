using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameObject menu;
    float tempTimescale = 1.0f;

    private void OnEnable()
    { 
        //Make sure previous is not 0 as to not cause multiple menus to override TempTimeScale
        if (Time.timeScale != 0)
        {
            tempTimescale = Time.timeScale;
        }

        Cursor.visible = true;

        Time.timeScale = 0.0f;
    }
    private void OnDisable()
    {
        Time.timeScale = tempTimescale;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {       
        if(!Cursor.visible)
        {
            Cursor.visible = true;
        }
        
        if (UnityEngine.Cursor.lockState != CursorLockMode.None)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
  
    }
}
