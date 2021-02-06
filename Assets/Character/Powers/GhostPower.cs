using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPower : MonoBehaviour
{
    //Power Ghost

    [Tooltip("Defines if the ghost power can be activated")]
    public bool ghostPowerActive = false;

    [Tooltip("Defines if the ghost power is active")]
    public bool isGhost = false;

    [Range(1, 5)]
    [Tooltip("Defines how long is remaining for the active ghost Power")]
    public float ghostTimer;

    [Range(1, 5)]
    [Tooltip("Defines how long is the maximum time a ghost power can be active")]
    public float ghostTimerMax = 3.0f;

    [Range(0, 3)]
    [Tooltip("Defines to what scale the ghostTimer regenerates")]
    public float ghostTimerRegenMultipler = 1.0f;

    [Tooltip("Defines how the material will change as a ghost")]
    public Material[] ghostMaterial;

    public void HandleGhostPower(bool IsActive = false, bool IsTriggered = false)
    {
        if (IsActive)
        {
            //If a ghost
            UpdateGhostPower(isGhost, IsTriggered);
        }
    }

    private void UpdateGhostPower(bool isGhost, bool IsTriggered = false, int BaseLayer = 0, int IgnoredLayer = 8)
    {

        if (isGhost)
        {
            InteractAsGhost();
        }
        else //If not a ghost
        {
            //Restart collision with passable layer
            Physics.IgnoreLayerCollision(BaseLayer, IgnoredLayer, false);

            //If Ghost power is triggered
            if (IsTriggered && (ghostTimer==ghostTimerMax))
            {
                MakeGhost();
            }
            else
            {
                RegenerateGhostTimer();
            }
        }
    }

    //Disable collision between baselayer and ignoredlayer while timer >0
    private void InteractAsGhost(int BaseLayer = 0, int IgnoredLayer = 8, bool HasTimeLimit = true)
    {
        //Turn off collision with passable terrain
        Physics.IgnoreLayerCollision(BaseLayer, IgnoredLayer);

        if (HasTimeLimit)
        {
            //Start counting down the ghost timer
            ghostTimer -= Time.deltaTime;

            //If you ghost timer -ve
            if (ghostTimer <= 0.1f)
            {
                StopBeingGhost();
            }
        }
    }

    private void StopBeingGhost()
    {
        //Come out of ghost form
        isGhost = false;

        //Go back to default look
        //GetComponent<Renderer>().material = standardMaterial;
    }

    private void MakeGhost()
    {
        //become a ghost
        isGhost = true;

        //Change look into ghosty form
        //GetComponent<Renderer>().material = ghostMaterial;
    }

    private void RegenerateGhostTimer(float amount = 0)
    {
        //Go up by given amount
        ghostTimer += amount;

        //...increase the ghost time avaliable
        ghostTimer += ghostTimerRegenMultipler * Time.deltaTime;

        //within cap
        if (ghostTimer > ghostTimerMax)
            ghostTimer = ghostTimerMax;
    }

}
