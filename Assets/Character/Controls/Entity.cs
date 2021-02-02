using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public CharacterController cController;

    public Movement MovementController;
    public GhostPower PowerGhost;
    public TimePower PowerTime;


    public bool Paused = false;
    private void Start()
    {
        MovementController = GetComponentInParent<Movement>();
    }

}
