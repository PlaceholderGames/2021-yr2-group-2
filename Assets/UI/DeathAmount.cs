using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAmount : MonoBehaviour
{
    Text _deaths;
    static string num = 0.ToString();
    // Start is called before the first frame update
    void Start()
    {
        _deaths = gameObject.GetComponent<Text>();
    }

    public void deathInc(int d)
    {
        num = d.ToString();
    }

    void Update()
    {
        _deaths.text = num;
    }
    
}
