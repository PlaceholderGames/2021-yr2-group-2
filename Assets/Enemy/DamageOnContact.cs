using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public PlayerController pcC;
    [Header("Timer")]
    [Range(0, 10)]
    [Tooltip("The time between damage")]
    public float timerMax = 1.0f;

    [Range(0, 10)]
    [Tooltip("Current countdown to new damage")]
    public float timerCur = 0.0f;

    [Header("Damage")]
    [Range(0, 30)]
    [Tooltip("Damage initally taken")]
    public int sharpDamage = 10;

    [Range(0, 10)]
    [Tooltip("Damage taken on end of countdown")]
    public int counterDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
        pcC = FindObjectOfType<PlayerController>();        
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
