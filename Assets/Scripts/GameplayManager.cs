using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{   
    public static GameplayManager instance;
   
    //TODO Needs to be set while loading.
    public LevelsInfo currentLevel;
    
    [SerializeField] private float enemyMoveDistance = .75f;

    public Action TurnSettledAction;
    public Action ContinueNextTurnAction;
    public Action WinGameAction;


    public LevelsInfo CurrentLevel {get {return currentLevel;}}

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        ContinueNextTurn();

        //float result = 0;

        // for(int i= 44; i > 0 ; i--)
        // {
        //     float val = ((float)i)/2;
            
        //    /// result += FloatRound(val);

        //     //result += Mathf.Round(i/2);
        //     result += i;
        // }

        

       
        // //Debug.Log(Mathf.Round(2.5f));
        // Debug.Log(result);

    }

    public int FloatRound(float val)
    {
        int result = (int)val;

        float x = val *10;
        float leftValue = x %10;
        if(leftValue >= 5)
        {
            result++;
        }
        //Debug.Log("Given value = " + val + ",Result = "+result);
        return result;
    }

    public void ContinueNextTurn()
    {
        //Debug.Log("Continue Next Turn");
        // All enemies will advance 1 tile.
        GameplayEnemyManager.instance.EnemiesAdvance(enemyMoveDistance);
        // If there's still needs to spawn enemies just spawn enemies.
        bool spawnerHasEnemies = EnemySpawnManager.instance.HasSpawnSlot();
        if(spawnerHasEnemies)
        {
            EnemySpawnManager.instance.SpawnEnemies();
        }
        
        ContinueNextTurnAction?.Invoke();
        TurnSettled();
    }


    public void GameWin()
    {
        Debug.Log("WON GAME");
        WinGameAction?.Invoke();
    }

    public void TurnSettled()
    {
        TurnSettledAction?.Invoke();
        PlayerController.instance.playerAbleToControl = true;
    }






}
