using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SixProjectileAbility",menuName = "Abilities/SixProjectileAbility",order =2)]
public class SpawnSixProjectileAbility : Abilities
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector3 leftDownStartPosition;
    [SerializeField] private float distance;

    public override void UseAbility()
    {
        OneWayAbilityProjectile.damage = GameDataManager.instance.GetRockSkillDamage();
        for(int i=0; i< 6;i++)
        {
            GameObject obj = Instantiate(projectilePrefab,(leftDownStartPosition + (Vector3.right * i * distance)),Quaternion.identity);
            //Set the damage of projectiles.
        }
    }
    
}
