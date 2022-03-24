using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayAbilityProjectile : MonoBehaviour
{
    
    public static float speed = 5f;
    public static float damage = 1;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject particlePos;
    [SerializeField] private AudioClip hitSound;


    private void Start() 
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }


    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<Health>(out Health targetHealt))
        {
            targetHealt.TakeDamage(damage);
            //particle effect
            GameObject particle = Instantiate(hitParticle,particlePos.transform.position,Quaternion.identity);   
            particle.transform.up = transform.up; 
            AudioSource.PlayClipAtPoint(hitSound,transform.position,1f);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Borders"))  Destroy(gameObject);


        if(other.gameObject.CompareTag("ProjectileKiller")) Destroy(gameObject);

        //particle effect.
        
    }

    


}
