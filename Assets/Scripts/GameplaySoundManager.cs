using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySoundManager : MonoBehaviour
{
    
    public static GameplaySoundManager instance;


    private void Awake() 
    {
        instance  = this;
    }


    public void PlayProjectileSound()
    {

    }

    public void PlayEnemyDeathSound()
    {

    }

    public void PlayProjectileHitSound()
    {

    }

    public void PlayThunderHitSound()
    {
        
    }
}
