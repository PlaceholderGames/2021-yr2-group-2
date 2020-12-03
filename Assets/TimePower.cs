using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePower : MonoBehaviour
{

    [Tooltip("Defines if the time power can be activated")]
    public bool TimePowerActive;


    [Tooltip("Defines if the character is slowed")]
    public bool IsSlowed;

    [Range(0, 50)]
    [Tooltip("Define the speed of the character when slowed")]
    public float slowMoveSpeed = 11;

    [Range(0, 2)]
    [Tooltip("Define the standard effect of delta time for the character")]
    public float standardTime = 1.0f;

    [Range(0, 2)]
    [Tooltip("Define the standard effect of delta time for the character")]
    public float slowTime = 0.5f;

    public PlayerController Player;

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
        {
            IsSlowed = true;
            SetTimeSlow(slowMoveSpeed, SlowTime, StandardTime);
        }
        else
        {
            if (Input.GetButtonUp("Time"))
            {
                IsSlowed = false;
                SetTimeSlow(Player.baseMoveSpeed);
            }
        }
    }

    private void SetTimeSlow(float Speed, float SlowTime = 1.0f, float StandardTime = 1.0f)
    {
        Time.timeScale = SlowTime;
        Player.moveSpeed = Speed;
    }

}
