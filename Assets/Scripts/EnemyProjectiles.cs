using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectiles : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float damage;

    public void SetSpeed(float speed,float dmg)
    {
        rb.velocity = transform.up * speed;
        damage = dmg;
    }

    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }
    }





}
