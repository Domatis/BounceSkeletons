using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="DamageAllEnemies",menuName ="Abilities/DamageAllEnemies",order = 1)]
public class DamageAllEnemiesAbility : Abilities
{
   
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private AudioClip hitSound;

   public override void UseAbility()
   {
      List<Enemies> currentEnemies =  GameplayEnemyManager.instance.CurrentEnemies;
       
       for(int i=0; i < currentEnemies.Count;i++)
       {
           
           GameObject particle = Instantiate(particleEffect,currentEnemies[i].transform.position,Quaternion.identity);
           AudioSource.PlayClipAtPoint(hitSound,particle.transform.position);   //Make Sound
           currentEnemies[i].GetComponent<Health>().TakeDamage(GameDataManager.instance.GetThunderSkillDamage());
       }
   }

}
