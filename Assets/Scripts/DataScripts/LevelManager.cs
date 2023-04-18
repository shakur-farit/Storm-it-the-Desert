using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Medals medals = new Medals();
    public int totalEnemy, enemyKilled, totalRescue, humanRescue,score;
    public UnityEvent onGameEnd;

    private string levelName;

    private void Awake()
    {
        instance = this;
        medals.untouched = true;

        levelName = SceneManager.GetActiveScene().name;
    }

    public void RegisterEnemy()
    {
        totalEnemy++;
    }

    public void RegisterRescue()
    {
        totalRescue++;
    }

    public void AddEnemyKill(int scorevalue)
    {
        enemyKilled++;
        score += scorevalue;
        UpdateScore.instance.DisplayScore(score);

    }

    public void AddRescue()
    {
        humanRescue++;
        score += 100;
        UpdateScore.instance.DisplayScore(score);
        StatsManager.instance.AddMoney(10);
        UpdateMoney.instance.DisplayMoney(StatsManager.instance.money);
    }

    public void PlayerHit()
    {
        medals.untouched = false;
    }

    public void GameEnd()
    {
        StartCoroutine(CountDelay());
    }

    private IEnumerator CountDelay()
    {
        yield return new WaitForSeconds(0.25f);

        if (enemyKilled >= totalEnemy)
            medals.kill = true;

        if (humanRescue >= totalRescue)
            medals.rescue = true;

        StatsManager.instance.AddMeadls(levelName, medals);
        StatsManager.instance.levelCompleted.Add(levelName, true);
        StatsManager.instance.SaveProgress();

        onGameEnd.Invoke();
    }
}

[System.Serializable]
public class Medals
{
    public bool rescue, kill, untouched;
}
