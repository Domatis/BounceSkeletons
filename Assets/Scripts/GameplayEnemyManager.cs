using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEnemyManager : MonoBehaviour
{

    
    public static GameplayEnemyManager instance;


    private List<Enemies> currentEnemies =new List<Enemies>();



    private void Awake() 
    {
        instance = this;     
    }

    public void EnemiesAdvance(float distance)
    {
        for(int i = 0; i< currentEnemies.Count; i++)
        {
            currentEnemies[i].MakeMovement(distance);
        }
    }


    public void AddEnemy(Enemies enemy)
    {
        currentEnemies.Add(enemy);
    }

    public void RemoveEnemy(Enemies enemy)
    {
        currentEnemies.Remove(enemy);
        if(currentEnemies.Count <= 0 && !EnemySpawnManager.instance.HasSpawnSlot()) GameplayManager.instance.GameWin();
    }

    public int GetCurrentEnemyCount()
    {
        return currentEnemies.Count;
    }

}
