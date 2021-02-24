using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAmount : MonoBehaviour
{
    Text _deaths;
    // Start is called before the first frame update
    void Start()
    {
        _deaths = gameObject.GetComponent<Text>();
    }

    public void deathInc(int d)
    {
        _deaths.text = d.ToString();
    }
}
