using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{   
    public static GameplayManager instance;
   
    //TODO Needs to be set while loading.
    
    
    [SerializeField] private float enemyMoveDistance = .75f;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Transform playerRightBorder;
    [SerializeField] private Transform playerLeftBorder;   

    public Action TurnSettledAction;
    public Action ContinueNextTurnAction;
    public Action WinGameAction;
    public Action LoseGameAction;

    private LevelsInfo currentLevel;

    public LevelsInfo CurrentLevel {get {return currentLevel;}}
    public Health PlayerHealth {get{return playerHealth;}}

    private void Awake() 
    {
        instance = this;
        currentLevel = GameDataManager.instance.GetCurrentLevel();
    }

    private void Start() 
    {   
        Time.timeScale = 1;     //Always set default at start.
        ContinueNextTurn();
        playerHealth.healthBecameZero += GameLose;
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
            GameplayUIManager.instance.updateLevelUI();
        }
        
        ContinueNextTurnAction?.Invoke();
        TurnSettled();
    }

    public void SkipNextTurn()
    {
        ProjectileSpawner.instance.DestroyAllProjectiles();
    }


    public void GameWin()
    {
        GameDataManager.instance.IncreasePlayerGold(currentLevel.levelPrize);   // Update the player coin value.
        GameDataManager.instance.LevelCompleted(currentLevel.Level);            // Unlocks levels.
        WinGameAction?.Invoke();
        GameplayUIManager.instance.GameWinUI();
    }

    public void GameLose()
    {
        Debug.Log("Game Lose");
        LoseGameAction?.Invoke();
        GameplayUIManager.instance.GameLoseUI();
    }

    public void TurnSettled()
    {
        TurnSettledAction?.Invoke();
        //Place player cannon randomly.
        float xPos = UnityEngine.Random.Range(playerLeftBorder.position.x,playerRightBorder.position.x);
        Vector3 playerPos = PlayerController.instance.playerCannon.transform.position;
        playerPos.x = xPos;
        PlayerController.instance.playerCannon.transform.position = playerPos;
        PlayerController.instance.playerCannon.transform.rotation = Quaternion.identity;
        PlayerController.instance.playerAbleToControl = true;
    }






}
