using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
   
    public static GameplayManager instance;
    [SerializeField] private float enemyMoveDistance = .75f;

    public Action TurnSettledAction;
    public Action ContinueNextTurnAction;
    public Action WinGameAction;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        ContinueNextTurn();
    }


    public void ContinueNextTurn()
    {
        Debug.Log("Continue Next Turn");
        // All enemies will advance 1 tile.
        GameplayEnemyManager.instance.EnemiesAdvance(enemyMoveDistance);
        // If there's still needs to spawn enemies just spawn enemies.
        bool spawnerHasEnemies = EnemySpawnManager.instance.CanSpawnEnemies();
        if(!spawnerHasEnemies && GameplayEnemyManager.instance.GetCurrentEnemyCount() == 0)
        {
            Debug.Log("WON GAME");
            WinGameAction?.Invoke();
        }
        ContinueNextTurnAction?.Invoke();
        TurnSettled();
    }

    public void TurnSettled()
    {
        TurnSettledAction?.Invoke();
        PlayerController.instance.playerAbleToControl = true;
    }






}
