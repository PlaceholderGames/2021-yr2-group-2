using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public SpeedRun _time;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _time.delay = Time.time;
    }

    void OnDisable()
    {
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
