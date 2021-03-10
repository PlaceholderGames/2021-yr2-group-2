using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelSet : MonoBehaviour
{
    [SerializeField]
    bool LavaCompleted = false;


    [SerializeField]
    bool SwampCompleted = false;

    private void LateUpdate()
    {
        if (PlayerPrefs.GetInt("Swamp") >= 2 && !SwampCompleted)
        {
            Destroy(GameObject.FindGameObjectWithTag("Lvl Complete Pickup").gameObject);
            SwampCompleted = true;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Lava") >= 3 && !LavaCompleted)
        {
            Destroy(GameObject.FindGameObjectWithTag("Lvl Complete Pickup").gameObject);
            LavaCompleted = true;
        }
    }
}
