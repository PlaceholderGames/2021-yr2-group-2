using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRun : MonoBehaviour
{
    Text _timeText;
    //public Text _timerText;
    // Update is called once per frame

    void Start()
    {
        _timeText=gameObject.GetComponent<Text>();
    }

    void Update()
    {
        int minutes = Mathf.FloorToInt(Time.time / 60f);
        int seconds = Mathf.FloorToInt(Time.time - minutes * 60);

        string textTime= string.Format("{0:0}:{1:00}", minutes, seconds);

        _timeText.text=textTime;

    }
}
