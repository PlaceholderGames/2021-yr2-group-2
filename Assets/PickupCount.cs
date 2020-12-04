using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCount : MonoBehaviour
{
    //Progress bar for pickup
    ProgressBar pickup;

    //Completion colour
    [Tooltip("The colour the bar will go when complete")]
    public Color CompletionColour;

    //Contains script for next level
    public NextLevel nextLvl;

    private void Start()
    {
        //Get the pickup progress bar 
        pickup = GetComponentInParent<ProgressBar>();

        //Get the NextLevel script
        nextLvl = GetComponentInParent<NextLevel>();

        //Set the maximum to be the total number of collectibles in the level
        pickup.maximum = GameObject.FindGameObjectsWithTag("Lvl Complete Pickup").Length;
        
        //Set current to 0
        pickup.current = 0;

        //Set minimum to 0
        pickup.minimum = 0;
    }

    private void Update()
    {
        //Set the current count to the maximum - number remaining
        pickup.current = pickup.maximum - GameObject.FindGameObjectsWithTag("Lvl Complete Pickup").Length;
        
        //When all collected change colour to clearly show all collected
        if (pickup.current == pickup.maximum)
        {
            pickup.fill_color = CompletionColour;
            nextLvl.GoToNextLevel();
        }

    }
}
