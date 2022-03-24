using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
   
    public static EnemySpawnManager instance;


    [SerializeField] private int turnCount = 10;
    [SerializeField] private int enemySlotCount = 6;
    [SerializeField] private float enemySnap = .75f;
    [SerializeField] private Transform upperLeftPosition;
    
    private int currentTurn;
    private int currentSpawnedEnemies = 0;

    private void Awake() 
    {
        instance = this;
        currentTurn = 1;
    }

    public bool HasSpawnSlot()
    {
        if(currentTurn > turnCount) return false;
        else return true;
    }


    public void SpawnEnemies()
    {
        currentSpawnedEnemies = 0;  //Reset this value.

        if(currentTurn == turnCount)
        {
            //BOSS spawn because of last turn.
            Vector3 position = upperLeftPosition.position + (Vector3.right * 2.5f * enemySnap) + (Vector3.up * 0.5f * enemySnap);
            GameObject bossEnemy = Instantiate(GameplayManager.instance.CurrentLevel.bossPrefab,position,Quaternion.identity);
            GameplayEnemyManager.instance.AddEnemy(bossEnemy.GetComponent<Enemies>());
            currentTurn++;
            return;
        }

        for(int i = 0; i < enemySlotCount; i++)
        {
            //With randomly we will create a enemy at this position or not.
            int randomnum = Random.Range(0,100);    //101 exclusive.
            if(randomnum < GameplayManager.instance.CurrentLevel.spawnRate)
            {   
                //Check for enemies spawned at maximum count and just return.
                if(currentSpawnedEnemies >= GameplayManager.instance.CurrentLevel.maxEnemySpawnAtOneTurn)
                {
                    currentTurn++;
                    return;
                }
                //SpawnEnemy
                int spawnIndex = Random.Range(0,GameplayManager.instance.CurrentLevel.enemyPrefabs.Length);
                GameObject enemyObj = Instantiate(GameplayManager.instance.CurrentLevel.enemyPrefabs[spawnIndex],upperLeftPosition.position + (Vector3.right *i * enemySnap),Quaternion.identity);
                GameplayEnemyManager.instance.AddEnemy(enemyObj.GetComponent<Enemies>());   //Add this enemy to enemy manager.
                currentSpawnedEnemies++;
            }
        }
        //Also make sure at least one enemy has spawned.
        if(currentSpawnedEnemies == 0 )
        {
            //SpawnEnemy
            int spawnIndex = Random.Range(0,GameplayManager.instance.CurrentLevel.enemyPrefabs.Length);
            GameObject enemyObj = Instantiate(GameplayManager.instance.CurrentLevel.enemyPrefabs[spawnIndex],upperLeftPosition.position,Quaternion.identity);
            GameplayEnemyManager.instance.AddEnemy(enemyObj.GetComponent<Enemies>());   //Add this enemy to enemy manager.
            currentSpawnedEnemies++;
        }

        currentTurn++;
        //Debug.Log(currentSpawnedEnemies.ToString() + " Enemies has spawned");
        
    }
}
