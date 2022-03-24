using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
   
    public static ProjectileSpawner instance;

    [SerializeField] private float damage = 10f;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private float timeBetweenProjectileSpawn = .2f;
    [SerializeField] private GameObject projectilePrefab;

    //TODO create a list for currentactiveprojectiles if it's needed.

    private List<Projectiles> currentProjectiles = new List<Projectiles>();

    private int currentActiveProjectileCount = 0;
    private int currentSpawnedProjectile;
    private float fireTimer;
    private bool firingActive = false;

    private Vector2 currentDirection;
    


    private void Awake() {
        instance = this;
    }


    private void Start() 
    {
        numberOfProjectiles = GameDataManager.instance.GetProjectileCount();
        damage = GameDataManager.instance.GetProjectileDamage();

        Projectiles.damage = damage;
    }

    private void Update() 
    {
        if(firingActive)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= timeBetweenProjectileSpawn)
            {
                Projectiles projectile = Instantiate(projectilePrefab,PlayerController.instance.projectileSpawnPoint.transform.position,Quaternion.identity).GetComponent<Projectiles>();
                projectile.SetDirection(currentDirection);
                currentProjectiles.Add(projectile);
                fireTimer = 0;
                currentActiveProjectileCount ++;
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
       currentActiveProjectileCount = 0;
       fireTimer = timeBetweenProjectileSpawn; //We need to instatiate projectile at first.
   }

   public void DestroyAllProjectiles()
   {    
       if(firingActive) return;

        int size = currentProjectiles.Count;    //Needs to cache at first because count is changing after destroy projectiles.

       for(int i=0; i < size; i++)
       {
         currentProjectiles[0].DestroyProjectile();     //Always delete first one, because list is updating after destroy projectiles.
       }
   }    

    public void ProjectileDestroyed(Projectiles projectile)
    {
        currentProjectiles.Remove(projectile);
        currentActiveProjectileCount--;

        if(currentActiveProjectileCount <= 0 && !firingActive)  //There is no more projectiles on scene.
        {
            //send information to gameplay manager.
            GameplayManager.instance.ContinueNextTurn();
        }
    }

}
