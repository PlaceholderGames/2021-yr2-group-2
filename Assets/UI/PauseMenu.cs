using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject _optionsMenu;
    public CameraFollow _camera;
    bool _initialCall = true;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (_initialCall)
        {
            _initialCall = false;
        }
        else
        {
            _camera.enabled = false;
            Time.timeScale = 0.0f;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update() { 
        _camera.enabled = false;
        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeButtonFunc()
    {
        _camera.enabled = true;
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void OptionButtonClose()
    {
        _optionsMenu.SetActive(false);
        gameObject.SetActive(true);
    }
}
