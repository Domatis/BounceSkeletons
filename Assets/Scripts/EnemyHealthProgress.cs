using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="HealthProgress",menuName ="EnemyHealthProgress/HealthProgress",order =1)]
public class EnemyHealthProgress : ScriptableObject
{
   
    //Max 20 for this case.
    [SerializeField] private int[] healths;


    public int GetHealth(int currentLevel)
    {
        return healths[currentLevel-1];
    }
}
