using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsInstruction : MonoBehaviour
{

    public bool timePower = false;
    public bool ghostPower = false;

    public GameObject ghostUI = null;
    public GameObject timeUI = null;

    GhostPower gp = null;
    TimePower tp = null;

    // Start is called before the first frame update
    void Start()
    {
        gp = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostPower>();
        tp = GameObject.FindGameObjectWithTag("Player").GetComponent<TimePower>();
    }

    // Update is called once per frame
    void Update()
    {
        timePower = tp.TimePowerActive;
        ghostPower = gp.ghostPowerActive;

        ghostUI.SetActive(ghostPower);
        timeUI.SetActive(timePower);
    }
}
