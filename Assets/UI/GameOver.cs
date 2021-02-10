using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    bool _initialCall = true;
    public CameraFollow _camera;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        print(_initialCall);
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
}