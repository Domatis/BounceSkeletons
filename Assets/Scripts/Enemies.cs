using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemies : MonoBehaviour
{
   
    [SerializeField] private float damage;
    private void Start() 
    {
        GetComponent<Health>().healthBecameZero += DestroyEnemy;
    }

    public void DestroyEnemy()
    {
        GameplayEnemyManager.instance.RemoveEnemy(this);
        Destroy(gameObject);
    }

    public void MakeMovement(float movement)
    {
        transform.position += (Vector3.down * movement);
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {

        //If collide with player gate take damage to it.
        if(other.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            Debug.Log("Player Damaged");
            playerHealth.TakeDamage(damage);
            DestroyEnemy();
        }

    }
}
