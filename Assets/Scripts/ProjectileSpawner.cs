using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
   
    public static ProjectileSpawner instance;

    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float timeBetweenProjectileSpawn = .2f;
    [SerializeField] private GameObject projectilePrefab;

    private int currentLaunchedProjectile = 0;
    private float fireTimer;
    private bool firingActive = false;

    private Vector2 currentDirection;


    private void Awake() {
        instance = this;
    }

    private void Update() 
    {
        if(firingActive)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= timeBetweenProjectileSpawn)
            {
                GameObject projectile = Instantiate(projectilePrefab,PlayerController.instance.playerHolder.transform.position,Quaternion.identity);
                projectile.GetComponent<Projectiles>().SetDirection(currentDirection);
                fireTimer = 0;
                currentLaunchedProjectile ++;
                if(currentLaunchedProjectile >= numberOfProjectiles)
                {
                    firingActive = false;
                }
            }
            
        }
    }

   public void MakeFire(Vector2 direction)
   {
       firingActive = true;
       currentDirection = direction;
       currentLaunchedProjectile = 0;
       fireTimer = timeBetweenProjectileSpawn; //We need to instatiate projectile at first.
   }

    public void ProjectileDestroyed()
    {
        currentLaunchedProjectile--;
        if(currentLaunchedProjectile <= 0)  //There is no more projectiles on scene.
        {
            //send information to gameplay manager.
            GameplayManager.instance.ContinueNextTurn();
        }
    }

}
