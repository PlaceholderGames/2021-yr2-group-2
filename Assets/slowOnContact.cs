using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowOnContact : MonoBehaviour
{
    Entity pcC;

    [Range(0, 10)]
    [Tooltip("The time between slow reset")]
    public float timerMax;

    [Range(0, 10)]
    [Tooltip("The current time till slow reset")]
    public float timerCur;

    [Range(0, 1)]
    [Tooltip("Slow percentage of speed")]
    public float slowPercentage;

    [Range(0, 10)]
    [Tooltip("Damage taken on end of countdown")]
    public int minSlowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        pcC = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        timerCur -= Time.deltaTime;

        if (other.gameObject.name == "Player" && timerCur < 0.1f)
        {
            pcC.MovementController.moveSpeed = pcC.MovementController.moveSpeed * slowPercentage;

            if (pcC.MovementController.moveSpeed <= minSlowSpeed) pcC.MovementController.moveSpeed = minSlowSpeed;

            timerCur = timerMax;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            timerCur = timerMax;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            pcC.MovementController.moveSpeed = pcC.MovementController.baseMoveSpeed;
        }

    }
}
