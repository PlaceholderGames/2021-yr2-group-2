using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDisable()
    {
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
