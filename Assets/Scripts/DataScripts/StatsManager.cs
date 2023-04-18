using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public int lives = 5;
    public int money;

    [Header("Upgrade Options")]
    public List<ShootProfile> machinegunUpgradeList = new List<ShootProfile>();
    public List<ShootProfile> missileUpgradeList = new List<ShootProfile>();
    public List<float> healthUpgradeList = new List<float>();
    public List<MegaBombData> megaBombUpgradeList = new List<MegaBombData>();
    public List<ShieldData> shieldUpgradeList = new List<ShieldData>();
    public List<LaserData> laserUpgradeList = new List<LaserData>();

    public Dictionary<string, Medals> achievemntList = new Dictionary<string, Medals>();
    public Dictionary<string, bool> levelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> statsTimer = new Dictionary<string, DateTime>();

    [Header("Upgrade Timer Data")]
    public List<StatsUpgradeInfo> stats = new List<StatsUpgradeInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadProgress();
    }

    public void AddMoney(int value)
    {
        money += value;

        UpdateMoney.instance.DisplayMoney(money);
    }

    public T GetStatsValue<T>(string statName, List<T> statsList)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statName)
            {
                return statsList[stats[i].level - 1];
            }
        }

        return default(T);
    }

    public float[] GetUpgradeTime(string statName)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statName)
            {
                return stats[i].upgradeTime;
            }
        }

        return null;
    }

    public StatsUpgradeInfo GetStats(string statName)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == statName)
            {
                return stats[i];
            }
        }

        return null;
    }

    public void SaveProgress()
    {
        SaveData saveData = new SaveData();

        saveData.lives = lives;
        saveData.money = money;

        saveData.achievemntList = achievemntList;
        saveData.levelCompleted = levelCompleted;
        saveData.statsTimer = statsTimer;

        saveData.stats = stats;

        SaveSystem.Save(saveData);
    }

    public void LoadProgress()
    {
        SaveData loadData = SaveSystem.Load<SaveData>();

        lives = loadData.lives;
        levelCompleted = loadData.levelCompleted;
        money = loadData.money;

        achievemntList = loadData.achievemntList;
        statsTimer = loadData.statsTimer;

        stats = loadData.stats;

        UpdateItemDisplay();
    }

    private void UpdateItemDisplay()
    {
        UpgradeItem[] items = FindObjectsOfType<UpgradeItem>();
        LevelMenu[] levelMenus = FindObjectsOfType<LevelMenu>();

        for(int i = 0; i < items.Length; i++)
        {
            items[i].UpdateItemDisplay();
        }

        for (int i = 0; i < levelMenus.Length; i++)
        {
            levelMenus[i].UpdateMenu();
        }
    }

    public void AddMeadls(string levelName, Medals medal)
    {
        Medals newMedal = new Medals();

        if (achievemntList.ContainsKey(levelName))
        {
            newMedal.kill = medal.kill ? true : achievemntList[levelName].kill;
            newMedal.rescue = medal.rescue ? true : achievemntList[levelName].rescue;
            newMedal.untouched = medal.untouched ? true : achievemntList[levelName].untouched;

            achievemntList[levelName] = newMedal;
        }
        else
        {
            achievemntList.Add(levelName, medal);
        }   
    }
}

[System.Serializable]
public class StatsUpgradeInfo
{
    public string name;
    public int level;
    public float[] upgradeTime;
}

[System.Serializable]
public class MegaBombData
{
    public float radius, damage;
}

[System.Serializable]
public class ShieldData
{
    public float shieldDuration;
}

[System.Serializable]
public class LaserData
{
    public float laserDuration;
}

[System.Serializable]
public class SaveData
{
    public int lives;
    public int money;

    public Dictionary<string, Medals> achievemntList = new Dictionary<string, Medals>();
    public Dictionary<string, bool> levelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> statsTimer = new Dictionary<string, DateTime>();

    public List<StatsUpgradeInfo> stats = new List<StatsUpgradeInfo>();
}