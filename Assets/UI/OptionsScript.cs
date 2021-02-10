using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Slider _Mouse;
    public Slider _Volume;
    public GameObject _PauseMenu;

    //public GameObject _OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OptionButtonOpen()
    {
        _PauseMenu.SetActive(false);
        gameObject.SetActive(true);
    }
}

