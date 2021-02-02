using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelSet : MonoBehaviour
{
    public bool LavaCompleted;
    public bool SwampCompleted;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Lava") == 3 || LavaCompleted)
        {
            FindObjectOfType<LvlCollectorPickup>().curCount++;
        }

        if (PlayerPrefs.GetInt("Swamp") == 3 || SwampCompleted)
        {
            FindObjectOfType<LvlCollectorPickup>().curCount++;
        }
    }

    private void Update()
    {
        print("Lava" + PlayerPrefs.GetInt("Lava").ToString());

        print("Swamp" + PlayerPrefs.GetInt("Swamp").ToString());
    }
}
