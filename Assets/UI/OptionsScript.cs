using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Slider _Mouse;
    public Slider _Volume;
    public GameObject _PauseMenu;
    public CameraFollow _camera;
    public AudioSource _music;

    //public GameObject _OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //Adds a listener to the _Mouse slider and invokes a method when the value changes.
        _Mouse.onValueChanged.AddListener(delegate { MouseChangeCheck(); });

        //Adds a listener to the _Volume slider and invokes a method when the value changes.
        _Volume.onValueChanged.AddListener(delegate { VolumeChangeCheck(); });
    }

    void Update()
    {
        Time.timeScale = 0.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    public void MouseChangeCheck()
    {
        _camera.rotateSpeed = _Mouse.value;
    }

    public void VolumeChangeCheck()
    {
        _music.volume = _Volume.value;
    }

    public void OptionButtonOpen()
    {
        _PauseMenu.SetActive(false);
        gameObject.SetActive(true);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}

