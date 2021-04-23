using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class SpeedRun : MonoBehaviour
{
    //The text that will be affected from the update function
    Text _timeText;
    public Text _highScoreText;
    public static float delay = 0f;
    static float time = 0;
    static bool initialCall = true;

    private static float minutes = 0;
    private static float seconds = 0;

    static float highScore = 0;

    // Update is called once per frame

    void Start()
    {
        if (initialCall)
        {
            delay = Time.time;
            initialCall = false;
        }
        highScore = PlayerPrefs.GetFloat("HighScore");
        _timeText = gameObject.GetComponent<Text>(); //finds the text component
    }

    void Update()
    {

        if ( !(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            if (!(SceneManager.GetActiveScene().name == "WinScreen"))
            {
                minutes = Mathf.FloorToInt((Time.time - delay) / 60f);
                seconds = (float)Math.Round(((Time.time - delay) - minutes * 60), 2);
                time = (minutes * 60) + seconds;
            }
            else if (time < highScore)
            {
                highScore = time;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }
            if (_highScoreText != null) { _highScoreText.text = highScore.ToString(); }
        }
        _timeText.text = string.Format("{00:00}:{01:00}", minutes, seconds.ToString()); 

    }

    public void setDelay()
    {
        delay = Time.time;
        print(delay);
        print(Time.time);
    }
}
