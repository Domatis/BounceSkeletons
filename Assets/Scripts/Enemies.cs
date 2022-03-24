using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemies : MonoBehaviour
{
    
    public Action movementEnd;

    [SerializeField] private EnemyHealthProgress healthProgress;
    [SerializeField] private GameObject destroyParticleEffect;
    [SerializeField] private GameObject gateHitParticle;
    [SerializeField] private float damage;
    [SerializeField] private AudioClip deathSound;

    private void Start() 
    {
        Health health = GetComponent<Health>();
        health.healthBecameZero += DestroyEnemy;
        health.SetMaxHealth(healthProgress.GetHealth(GameplayManager.instance.CurrentLevel.Level));
    }

    public void DestroyEnemy()
    {
        //Make death sound.
        AudioSource.PlayClipAtPoint(deathSound,transform.position);
        Instantiate(destroyParticleEffect,transform.position,destroyParticleEffect.transform.rotation);
        GameplayEnemyManager.instance.RemoveEnemy(this);
        Destroy(gameObject);
    }

    public void MakeMovement(float movement)
    {
        transform.position += (Vector3.down * movement);
        movementEnd?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //If collide with player gate take damage to it.
        if(other.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            Instantiate(gateHitParticle,transform.position,gateHitParticle.transform.rotation);
            playerHealth.TakeDamage(damage);
            DestroyEnemy();
            
        }

    }
}
