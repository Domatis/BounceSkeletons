using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Abilities : ScriptableObject
{

   public enum AbilityTypes {thunderSkill,rockSkill}

   public Sprite abilitySprite;
   public AbilityTypes abilityType;

   public abstract void UseAbility();
   public Sprite GetSprite()
   {
      return abilitySprite;
   }
}
