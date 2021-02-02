using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent <Animation>()["BallAnimation"].time = Random.Range(0f , 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
