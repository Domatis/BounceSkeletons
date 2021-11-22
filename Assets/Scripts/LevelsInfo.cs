using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName = "CreateLevels/Level",order =1)]
public class LevelsInfo : ScriptableObject
{
   
    [Min(1)]
    public int Level = 1;
    [Range(0,100)]
    public float spawnRate;
    [Range(0,10)]
    public int maxEnemySpawnAtOneTurn;
    public GameObject[] enemyPrefabs;

}
