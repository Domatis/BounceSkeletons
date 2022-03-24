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
    public Action takeDamageAction;

    private float maxHealth = 100f;
    private float currentHealth;

    public float CurrentHealth {get{return currentHealth;}}

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
        takeDamageAction?.Invoke();
        //Update UI Elements
        healthImage.fillAmount = currentHealth/maxHealth;
        healthText.text = ((int)currentHealth).ToString();  //changing integer before update the text.
        if(currentHealth <= 0)
        {
            healthBecameZero?.Invoke();
        }
    }


}
