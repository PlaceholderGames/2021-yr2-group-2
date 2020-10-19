using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar Healthbar;



    // Start is called before the first frame update
    void Start()
    {
        Healthbar.slider = Healthbar.gameObject.GetComponent<Slider>();
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Healthbar.SetHealth(currentHealth);
    }
}
