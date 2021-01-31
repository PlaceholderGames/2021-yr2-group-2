using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Character_audioManager : MonoBehaviour
{

    public AudioSource ASJump;
    public AudioSource ASCoin;

    // Start is called before the first frame update
    void Start()
    {
        ASJump = gameObject.AddComponent<AudioSource>();
        ASCoin = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
