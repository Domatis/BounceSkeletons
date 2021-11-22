using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
   
    public static ProjectileSpawner instance;

    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float timeBetweenProjectileSpawn = .2f;
    [SerializeField] private GameObject projectilePrefab;

    //TODO create a list for currentactiveprojectiles if it's needed.

    private int currentActiveProjectile = 0;
    private int currentSpawnedProjectile;
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
                currentActiveProjectile ++;
                currentSpawnedProjectile++;
                if(currentSpawnedProjectile >= numberOfProjectiles)
                {
                    firingActive = false;
                    currentSpawnedProjectile = 0;
                }
            }
            
        }
    }

   public void MakeFire(Vector2 direction)
   {
       firingActive = true;
       currentDirection = direction;
       currentActiveProjectile = 0;
       fireTimer = timeBetweenProjectileSpawn; //We need to instatiate projectile at first.
   }

    public void ProjectileDestroyed()
    {
        currentActiveProjectile--;
        if(currentActiveProjectile <= 0 && !firingActive)  //There is no more projectiles on scene.
        {
            //send information to gameplay manager.
            GameplayManager.instance.ContinueNextTurn();
        }
    }

}
