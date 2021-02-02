using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject PauseMenu;

    public void Unpause()
    {
        PauseMenu.SetActive(false);
    }
}
