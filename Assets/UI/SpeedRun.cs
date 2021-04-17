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
    float startingTime = 0f;
    static float time = 0;

    private static float minutes = 0;
    private static float seconds = 0;

    static float highScore = 0;

    // Update is called once per frame

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore");
        print(highScore);
        _timeText = gameObject.GetComponent<Text>(); //finds the text component
    }

    void Update()
    {

        if (!(SceneManager.GetActiveScene().name == "WinScreen") && !(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            minutes = Mathf.FloorToInt((Time.time - startingTime) / 60f);
            seconds = (float)Math.Round(((Time.time - startingTime) - minutes * 60), 2);
            time = (minutes * 60) + seconds;
        }
        else
        {
            if (time < highScore)
            {
                print("here");
                highScore = time;
                PlayerPrefs.SetFloat("HighScore", highScore);
            }
            print(time);
            if (_highScoreText != null) { _highScoreText.text = highScore.ToString(); print(highScore); }
        }
        _timeText.text = string.Format("{00:00}:{01:00}", minutes, seconds.ToString()); 
        // _timeText.text = time.ToString();
    }
}
