using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenesManager : MonoBehaviour
{
   public static  GameScenesManager instance;
   
    private void Awake() 
    {
        instance = this;
    }
}
