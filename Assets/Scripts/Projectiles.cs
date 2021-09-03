using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectiles : MonoBehaviour
{
   
    [SerializeField] private float secUntilAdjustment = 10;
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private float timer = 0;
    private bool makeIntervention = false;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {   
        if(!makeIntervention)
        {
            timer += Time.deltaTime;
            if(timer >= secUntilAdjustment)
            {
                Debug.Log("Intervention started");
                makeIntervention = true;
            }
        }
    }

    public void SetDirection(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(makeIntervention)
        {
            rb.velocity = (rb.velocity + new Vector2(Random.Range(-2f,2f),Random.Range(-2f,-1))).normalized * speed * 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("ProjectileKiller"))
        {
            Debug.Log("Projectile Destroyed");
            Destroy(gameObject);
        }
    }

}
