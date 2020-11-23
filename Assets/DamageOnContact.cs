using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public PlayerController pcC;

    [Range(0,10)]
    [Tooltip("The time between damage")]
    public float timerMax;

    [Range(0,10)]
    [Tooltip("Current countdown to new damage")]
    public float timerCur;

    [Range(0, 30)]
    [Tooltip("Damage initally taken")]
    public int sharpDamage;

    [Range(0, 10)]
    [Tooltip("Damage taken on end of countdown")]
    public int counterDamage;

    // Start is called before the first frame update
    void Start()
    {
        pcC = FindObjectOfType<PlayerController>();        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        timerCur -= Time.deltaTime;

        if (other.gameObject.name == "Player" && timerCur < 0.1f)
        {
            pcC.TakeDamage(counterDamage);
            timerCur = timerMax;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            timerCur = timerMax;
            pcC.TakeDamage(sharpDamage);
        }
    }
}
