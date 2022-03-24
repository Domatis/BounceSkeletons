using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectiles : MonoBehaviour
{
   
     
     public static float damage;
    [SerializeField] private float secUntilAdjustment = 10;
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject destroyParticle;
    [Header("Sounds")]
    [SerializeField] private AudioSource asource;
    [SerializeField] private AudioClip releaseSound;
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip defaultHitSound;

    private Rigidbody2D rb;
    private float timer = 0;
    private bool makeIntervention = false;
    private bool projectileDestroyed = false;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start() 
    {

        //Make release sound.
        asource.clip = releaseSound;
        asource.Play();
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

        transform.up = rb.velocity.normalized;
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

        if(other.gameObject.TryGetComponent<Health>(out Health targetHealt))
        {
            targetHealt.TakeDamage(damage);
            //particle part.
            GameObject particle = Instantiate(hitParticle,other.GetContact(0).point,Quaternion.identity);   
            particle.transform.up = transform.up;    
            //Make enemy hit sound.
            asource.PlayOneShot(enemyHitSound,.3f);
        }

        else
        {
            //other collisions make default hit sound.
            asource.PlayOneShot(defaultHitSound,.15f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("ProjectileKiller"))
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        if(projectileDestroyed) return; //Prevent the multiple collisions.
        projectileDestroyed = true;
        ProjectileSpawner.instance.ProjectileDestroyed(this);
        Instantiate(destroyParticle,transform.position,destroyParticle.transform.rotation);
        Destroy(gameObject);
    }

}
