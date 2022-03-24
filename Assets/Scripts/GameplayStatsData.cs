using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="statData",menuName ="StatsData/statsdata",order =2)]
public class GameplayStatsData : ScriptableObject
{
    public Stats[] stats;
}

[System.Serializable]
public class Stats
{
    public int costValue = 0;
    public int statData = 0;
}
