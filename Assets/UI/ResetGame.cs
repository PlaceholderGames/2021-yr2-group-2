using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void ResetGamePrefs()
    {
        PlayerPrefs.SetInt("Lava", 0);
        PlayerPrefs.SetInt("Swamp", 0);
    }
}
