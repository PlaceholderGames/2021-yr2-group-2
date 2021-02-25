﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SpeedRun : MonoBehaviour
{
    //The text that will be affected from the update function
    Text _timeText;
    static string textTime;

    // Update is called once per frame

    void Start()
    {
        _timeText=gameObject.GetComponent<Text>(); //finds the text component
    }

    void Update()
    {
        if (!(SceneManager.GetActiveScene().name == "WinScreen"))
        {
            int minutes = Mathf.FloorToInt(Time.time / 60f);
            string seconds = (Math.Round((Time.time - minutes * 60), 2)).ToString();
            textTime = string.Format("{00:00}:{01:00}", minutes, seconds);
        }
        _timeText.text = textTime;
        

    }
}
