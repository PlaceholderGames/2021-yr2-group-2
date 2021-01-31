using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetText : MonoBehaviour
{
    TextMeshProUGUI TMP;
    ProgressBar ProBar;

    [Tooltip("Value for 'Current' count")]
    public float Current = 0f;

    [Tooltip("Value for 'Maximum' count")]
    public float Max;

    [Range(0, 5)]
    [Tooltip("Max time between swap of ShownText")]
    public float TimerMax;

    [Range(0, 5)]
    [Tooltip("Current time till swap of ShownText")]
    public float TimerCurrent;

    [Tooltip("Showing Current || Maximum")]
    public bool ShowingCurrent;

    [Tooltip("Currently Shown Text")]
    public string ShownText;

    private void Start()
    {
        TMP = GetComponentInParent<TextMeshProUGUI>();

        if (TMP == null)
        {
            Debug.Log("Unable to find TextMeshPro for SetText Script");
        }

        ProBar = GetComponentInParent<ProgressBar>();

        if(ProBar == null)
        {
            Debug.Log("Unable to find ProgressBar for SetText Script");
        }

        TimerCurrent = TimerMax;
    }


    // Update is called once per frame
    void Update()
    {
        TimerCurrent -= Time.deltaTime;

        if(TimerCurrent <= 0.1f)
        {
            TimerCurrent = TimerMax;
            ShowingCurrent = !ShowingCurrent;

            
      
        }

        if (ShowingCurrent)
        {
            Current = ProBar.current;
            ShownText = Current.ToString();
        }
        else
        {
            Max = ProBar.maximum;
            ShownText = Max.ToString();
        }

        TMP.text = ShownText;
        
    }
}
