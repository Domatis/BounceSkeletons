using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager instance;

    [SerializeField] private GameplayStatsData playerHealthData;
    [SerializeField] private GameplayStatsData playerCannonDamageData;
    [SerializeField] private GameplayStatsData projectileCountData;
    [SerializeField] private GameplayStatsData thunderSkillData;
    [SerializeField] private GameplayStatsData rockSkillData;
    [Space] 
    [SerializeField] private Abilities[] allAbilities;
    [SerializeField] private LevelsInfo[] levels;


    //Keep current levels of each data.
    private int currentPlayerHealthLevel = 1;
    private int currentPlayerCannonLevel= 1;
    private int currentProjectileLevel= 1;
    private int currentThunderSkillLevel= 1;
    private int currentRockSkillLevel= 1;

    private int currentPlayerCoinValue = 0;
    private int currentSelectedLevel = 1;
    private int lastUnlockedLevel = 1;
    private int lastCompletedLevel = 0;


    private GameData gameData;

    private Abilities selectedAbility = null;

    public Abilities SelectedAbility {get{return selectedAbility;}}

    public int PlayerCoin 
    {
        get{return currentPlayerCoinValue;}
        set
        {
            currentPlayerCoinValue = value;
            SaveGameData();
        }  
    }

    public int SelectedLevel
    {
        get{return currentSelectedLevel;}
        set
        {
            currentSelectedLevel = value;
            SaveGameData();
        }
    }

    public int LastUnlockedLevel
    {
        get{return lastUnlockedLevel;}
    }   

    public int LastCompletedLevel
    {
        get{return  lastCompletedLevel;}
    }

    private void Awake() 
    {
        if(instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            selectedAbility = allAbilities[0];
        }
    }

    private void Start() 
    {
        

        gameData = LoadGameData();
        if(gameData == null)    //Have not created before.
        {
            gameData = new GameData();  //Create new one and use default values of data at first.
            selectedAbility = allAbilities[0];  //Must set.
        }

        else
        {
            //Update current values with loaded data.
            currentPlayerCoinValue = gameData.coinValue;
            currentPlayerHealthLevel = gameData.playerHealthlevel;
            currentPlayerCannonLevel = gameData.cannonlevel;
            currentProjectileLevel = gameData.projectilecountlevel;
            currentThunderSkillLevel  = gameData.thunderskilllevel;
            currentRockSkillLevel = gameData.rockskillevel;
            currentSelectedLevel = gameData.selectedlevel;
            lastUnlockedLevel = gameData.lastunlockedlevel;
            lastCompletedLevel = gameData.lastcompletedlevel;
            for(int i =0; i < allAbilities.Length;i++)
            {
                if((int)allAbilities[i].abilityType == gameData.selectedAbility)
                {
                    selectedAbility = allAbilities[i];
                    break;
                }
            }
        }

    }

    public void SaveGameData()
    {
        UpdateGameData();

        try{
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = new FileStream(Application.persistentDataPath + "/gamedata.dat",FileMode.Create);
            bf.Serialize(file,gameData);


            file.Close();
        }

        catch(Exception e)
        {
            
        }
    }

    public GameData LoadGameData()
    {
        GameData data = null;
        try
        {
            

            if(File.Exists(Application.persistentDataPath + "/gamedata.dat"))
            {
                FileStream file = new FileStream(Application.persistentDataPath+"/gamedata.dat",FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                try
                {
                    data = (GameData)bf.Deserialize(file);
                }

                catch
                {
                    data = null;
                }

                file.Close();
            }
        }

        catch(Exception e)
        {
             
        }

        return data;
    }

    //Update game data with current values.
    public void UpdateGameData()
    {
        gameData.coinValue = currentPlayerCoinValue;
        gameData.playerHealthlevel = currentPlayerHealthLevel;
        gameData.cannonlevel = currentPlayerCannonLevel;
        gameData.projectilecountlevel = currentProjectileLevel;
        gameData.thunderskilllevel = currentThunderSkillLevel;
        gameData.rockskillevel = currentRockSkillLevel;
        gameData.selectedlevel = currentSelectedLevel;
        gameData.lastunlockedlevel = lastUnlockedLevel;
        gameData.lastcompletedlevel = lastCompletedLevel;
        gameData.selectedAbility = (int)selectedAbility.abilityType;
    }

    private void OnApplicationQuit() 
    {
        SaveGameData();    
    }

    public void LevelCompleted(int level)
    {
        if(level > lastCompletedLevel)
        {
            lastCompletedLevel = level;
            lastUnlockedLevel = lastCompletedLevel+1;
        }
            
        SaveGameData();
    }

    public bool IsGoldEnough(int requiredGold)
    {
        if(requiredGold <= currentPlayerCoinValue) return true;
        //Else
        return false;
    }

    public LevelsInfo GetCurrentLevel()
    {
        return levels[currentSelectedLevel-1];
    }

    public void SetPlayerAbility(Abilities.AbilityTypes type)
    {
        for(int i=0; i<allAbilities.Length;i++)
        {
            if(allAbilities[i].abilityType == type)
            {
                selectedAbility = allAbilities[i];
                SaveGameData();
                return;
            }
        }
    }

    public Abilities GetSelectedAbility()
    {
        return selectedAbility;
    }

    public void UpgradePlayerHealth()
    {
        //Check there is enough gold for update or not.
        int cost = playerHealthData.stats[currentPlayerHealthLevel-1].costValue;
        if( cost > currentPlayerCoinValue) return;

        currentPlayerCoinValue -= cost;

        currentPlayerHealthLevel++;
        if(currentPlayerHealthLevel >= playerHealthData.stats.Length)
            currentPlayerHealthLevel = playerHealthData.stats.Length;
        

        SaveGameData();
    }


    public void UpgradePlayerCannonDamage()
    {
        //Check there is enough gold for update or not.
        int cost = playerCannonDamageData.stats[currentPlayerCannonLevel-1].costValue;
        if( cost > currentPlayerCoinValue) return;

        currentPlayerCoinValue -= cost;

        currentPlayerCannonLevel++;
        if(currentPlayerCannonLevel >= playerCannonDamageData.stats.Length)
            currentPlayerCannonLevel = playerCannonDamageData.stats.Length;
        

        SaveGameData();
    }

    public void UpgradeProjectileCount()
    {
        //Check there is enough gold for update or not.
        int cost = projectileCountData.stats[currentProjectileLevel-1].costValue;
        if( cost >currentPlayerCoinValue) return;

        currentPlayerCoinValue -= cost;

        currentProjectileLevel++;
        if(currentProjectileLevel >= projectileCountData.stats.Length)
            currentProjectileLevel = projectileCountData.stats.Length;
     

        SaveGameData();
    }

    private void UpgradeThunderskill()
    {
        //Check there is enough gold for update or not.
        int cost = thunderSkillData.stats[currentThunderSkillLevel-1].costValue;
        if( cost >currentPlayerCoinValue) return;

        currentPlayerCoinValue -= cost;
        currentThunderSkillLevel++;
        if(currentThunderSkillLevel >= thunderSkillData.stats.Length)      
            currentThunderSkillLevel = thunderSkillData.stats.Length;
        
        SaveGameData();

    }

    private void UpgradeRockSkill()
    {
        //Check there is enough gold for update or not.
        int cost = rockSkillData.stats[currentRockSkillLevel-1].costValue;
        if( cost >currentPlayerCoinValue) return;

        currentPlayerCoinValue -= cost;

        currentRockSkillLevel++;
        if(currentRockSkillLevel >= rockSkillData.stats.Length)
            currentRockSkillLevel = rockSkillData.stats.Length;
  
        SaveGameData();
    }

    public void UpgradeCurrentSelectedSkill()
    {
        if(selectedAbility.abilityType == Abilities.AbilityTypes.thunderSkill)
            UpgradeThunderskill();
        else UpgradeRockSkill();
    }

    public void IncreasePlayerGold(int value)
    {
        currentPlayerCoinValue += value;
        SaveGameData();
    }

    public Stats GetPlayerHealthData(out bool isMaxed)
    {   
        if(currentPlayerHealthLevel >= playerHealthData.stats.Length) isMaxed = true;
        else isMaxed = false;

        return playerHealthData.stats[currentPlayerHealthLevel-1];
    }

    public Stats GetCannonData(out bool isMaxed)
    {
        if(currentPlayerCannonLevel >= playerCannonDamageData.stats.Length) isMaxed = true;
        else isMaxed = false;

        return playerCannonDamageData.stats[currentPlayerCannonLevel-1];
    }

    public Stats GetProjectileData(out bool isMaxed)
    {
        if(currentProjectileLevel >= projectileCountData.stats.Length) isMaxed = true;
        else isMaxed = false;

        return projectileCountData.stats[currentProjectileLevel-1];
    }

    public Stats GetThunderSkillData(out bool isMaxed)
    {
        if(currentThunderSkillLevel >= thunderSkillData.stats.Length) isMaxed = true;
        else isMaxed = false;

        return thunderSkillData.stats[currentThunderSkillLevel-1];
    }

    public Stats GetRockSkillData(out bool isMaxed)
    {
        if(currentRockSkillLevel >= rockSkillData.stats.Length) isMaxed = true;
        else isMaxed = false;

        return rockSkillData.stats[currentRockSkillLevel-1];
    }


    public int GetPlayerHealth()
    {
        return playerHealthData.stats[currentPlayerHealthLevel-1].statData;
    }

    public int GetProjectileDamage()
    {
        return playerCannonDamageData.stats[currentPlayerCannonLevel-1].statData;
    }

    public int GetProjectileCount()
    {
        return projectileCountData.stats[currentProjectileLevel-1].statData;
    }

    public int GetThunderSkillDamage()
    {
        return thunderSkillData.stats[currentThunderSkillLevel-1].statData;
    }

    public int GetRockSkillDamage()
    {
        return rockSkillData.stats[currentRockSkillLevel-1].statData;
    }

    
}


[System.Serializable]
public class GameData
{
    public int coinValue,playerHealthlevel,cannonlevel,projectilecountlevel,thunderskilllevel,rockskillevel,selectedlevel,lastunlockedlevel,lastcompletedlevel;
    public int selectedAbility;
}
