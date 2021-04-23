using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetLevel : MonoBehaviour
{
    public int FirstLevel;
    public SpeedRun _time;

    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevel);
        if (_time != null)
        {
            _time.setDelay();
        }
        print("restart");
    }

    
}
