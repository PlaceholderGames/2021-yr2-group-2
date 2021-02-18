using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowOnContact : MonoBehaviour
{
    [Header("Effect Info")]
    [Range(0, 1)]
    [Tooltip("Slow percentage of speed")]
    [SerializeField]
    private float slowPercentage = 0.3f;

    [Range(0, 10)]
    [Tooltip("Damage taken on end of countdown")]
    [SerializeField]
    private int minSlowSpeed = 1;

    [Header("Timer")]
    [Range(0, 10)]
    [Tooltip("The time between slow reset")]
    [SerializeField]
    private float timerMax = 0.75f;

    [Range(0, 10)]
    [Tooltip("The current time till slow reset")]
    [SerializeField]
    private float timerCur = 0.0f;

    [Header("References")]
    [Tooltip("Reference to player")]
    [SerializeField]
    private Entity player = null;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        //If player not found notify console
        if(player == null)
        {
            Debug.LogError(this.gameObject.name + ": player not found");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Update Timer
        timerCur -= Time.deltaTime;

        //When timer expires on the player
        if (timerCur < 0.1f)
        {
            //Effect the player
            if (other.gameObject.name == "Player")
            {
                //Slow player movement by slow percentage
                player.MovementController.moveSpeed = player.MovementController.moveSpeed * slowPercentage;

                //Clamp movement speed by a minimum value
                if (player.MovementController.moveSpeed <= minSlowSpeed) player.MovementController.moveSpeed = minSlowSpeed;

                //Reset Timer
                timerCur = timerMax;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the player has has entered the trigger
        if (other.gameObject.name == "Player")
        {
            //Reset the timer
            timerCur = timerMax;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player has left the trigger reset their speed
        if(other.gameObject.name == "Player")
        {
            player.MovementController.moveSpeed = player.MovementController.baseMoveSpeed;
        }

        //Reset timer
        timerCur = timerMax;

    }
}
