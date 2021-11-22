using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePlace;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    
    private void Start() 
    {
        GetComponent<Enemies>().movementEnd += MakeFire;
    }

    public void MakeFire()
    {
        //Create prefab, set speed.
        EnemyProjectiles projectile = Instantiate(projectilePrefab.gameObject,projectilePlace.transform.position,projectilePrefab.transform.rotation).GetComponent<EnemyProjectiles>();
        projectile.SetSpeed(projectileSpeed,projectileDamage);
    }




}
