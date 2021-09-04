using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
   
    public static EnemySpawnManager instance;


    [SerializeField] private int turnCount = 10;
    [SerializeField] private int maxEnemySpawn = 6;
    [SerializeField] private float enemySnap = .75f;
    [SerializeField] private Transform upperLeftPosition;
    [SerializeField] private GameObject enemyPrefab;
    
    


    private int currentTurn;

    private void Awake() 
    {
        instance = this;
        currentTurn = 1;
    }


    public bool CanSpawnEnemies()
    {
        if(currentTurn > turnCount) return false;

        //Else
        for(int i = 0; i < maxEnemySpawn; i++)
        {
            //TODO This system will change.
            //With randomly we will create a enemy at this position or not.
            int randomnum = Random.Range(0,100);    //101 exclusive.
            if(randomnum < 50)  // %50 chance to create. 
            {
                GameObject enemyObj = Instantiate(enemyPrefab,upperLeftPosition.position + (Vector3.right *i * enemySnap),Quaternion.identity);
                GameplayEnemyManager.instance.AddEnemy(enemyObj.GetComponent<Enemies>());
                //Add this enemy to enemy manager.
            }
        }

        currentTurn++;

        return true;

    }




}
