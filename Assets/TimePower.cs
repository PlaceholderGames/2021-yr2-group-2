using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePower : MonoBehaviour
{


    GameObject TimeFilter;
    [Tooltip("Defines if the time power can be activated")]
    public bool TimePowerActive;

    [Tooltip("Defines if the Time power is active")]
    public bool isTimeActive = false;

    [Tooltip(" Defines how long the time power can be activated.")]
    public float TimePowerMax = 3.0f;

    [Tooltip("Defines if the character is slowed")]
    public bool IsSlowed;

    [Range(0, 50)]
    [Tooltip("Define the speed of the character when slowed")]
    public float slowMoveSpeed = 11;

    [Range(1, 5)]
    [Tooltip("Defines how long is remaining for the active ghost Power")]
    public float TimeTimer;

    [Range(0, 2)]
    [Tooltip("Define the standard effect of delta time for the character")]
    public float standardTime = 1.0f;

    [Range(0, 2)]
    [Tooltip("Define the standard effect of delta time for the character")]
    public float slowTime = 0.5f;

    [Range(0, 3)]
    [Tooltip("Defines to what scale the TimeTimer regenerates")]
    public float TimeTimerRegenMultipler = 1.0f;

    public Entity ControlledEntity;

    private void Start()
    {
        ControlledEntity = GetComponentInParent<Entity>();
        TimeFilter = GameObject.Find("TimeFilter");
        TimeFilter.SetActive(false);
    }

    public void HandleTimePower(bool IsActive = false, bool IsTriggered = false)
    {
        if (IsActive)
        {
            UpdateTimePower(IsTriggered, slowTime);
        }
    }

    private void UpdateTimePower(bool IsTriggered = false, float SlowTime = 1.0f, float StandardTime = 1.0f)
    {
        if (IsTriggered)
            IsSlowed = true;
        if (IsSlowed)
        {
            SetTimeSlow(slowMoveSpeed, SlowTime, StandardTime);
            TimeFilter.SetActive(true);
            TimeTimer -= Time.deltaTime * 2;

            //If you Time timer -ve
            if (TimeTimer <= 0.1f)
            {
                IsSlowed = false;
            }
        }
        else
        {


            SetTimeSlow(ControlledEntity.MovementController.baseMoveSpeed);
            TimeFilter.SetActive(false);
            RegenerateTimeTimer();

        }
        if (Input.GetButtonUp("Time"))
        {
            IsSlowed = false;
        }
    }

    private void SetTimeSlow(float Speed, float SlowTime = 1.0f, float StandardTime = 1.0f)
    {
        Time.timeScale = SlowTime;
        ControlledEntity.MovementController.moveSpeed = Speed;
    }

    private void RegenerateTimeTimer(float amount = 0)
    {
        //Go up by given amount
        TimeTimer += amount;

        //...increase the time avaliable
        TimeTimer += TimeTimerRegenMultipler * Time.deltaTime;

        //within cap
        if (TimeTimer > TimePowerMax)
            TimeTimer = TimePowerMax;
    }

}

