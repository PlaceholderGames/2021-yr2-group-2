using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public PlayerController pController;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private WaitForSeconds healthregentick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static Healthbar instance;

    private void Awake()
    {
        instance = this;

        //Find the Player
        GameObject obj = GameObject.Find("Player");
        pController = obj.GetComponent<PlayerController>();

        //Error message
        if(obj == null)
        {
            print("Could not find Player for PlayerController");
        }

        SetMaxHealth(pController.maxHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void TakeDamage()
    {

        slider.value = pController.currentHealth;
        if (regen != null)
        {
            StopCoroutine(regen);
        }
        regen = StartCoroutine(Regenhealth());
    }

    private IEnumerator Regenhealth()
    {
        yield return new WaitForSeconds(2);
        while (pController.currentHealth < pController.maxHealth)
        {
            pController.currentHealth += pController.maxHealth /100;
            slider.value = pController.currentHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return healthregentick;
        }

        regen = null;
    }
   


}
