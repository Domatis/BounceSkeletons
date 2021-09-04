using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
   
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image healthImage;
    [SerializeField] private Text healthText;

    public Action healthBecameZero;


    private float currentHealth;

    private void Start() 
    {
        currentHealth = maxHealth;
        healthText.text = maxHealth.ToString();
    }




    public void TakeDamage(float damageVal)
    {
        //TODO there will be health ui needs to be updated.
        Debug.Log("Target Got Damage");
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
