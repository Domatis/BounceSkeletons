using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
   
    
    [SerializeField] private Image healthImage;
    [SerializeField] private Text healthText;

    public Action healthBecameZero;

    private float maxHealth = 100f;
    private float currentHealth;

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        healthText.text = maxHealth.ToString();
    }


    public void TakeDamage(float damageVal)
    {
        //TODO there will be health ui needs to be updated.
        currentHealth -= damageVal;
        currentHealth = Mathf.Max(0f,currentHealth);    //Make sure the smallest value is zero.
        //Update UI Elements
        healthImage.fillAmount = currentHealth/maxHealth;
        healthText.text = ((int)currentHealth).ToString();  //changing integer before update the text.
        if(currentHealth <= 0)
        {
            healthBecameZero?.Invoke();
        }
    }


}
