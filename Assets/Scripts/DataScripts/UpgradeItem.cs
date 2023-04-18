using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItem : MonoBehaviour
{
    [Header("Upgrade Menu Objects")]
    public string statName;
    public string itemName;
    public TMP_Text itemNameText, buyText;
    public Slider itemLevelBar;
    public Button buyButton;

    [Header("Item Prices Setup:")]
    public int[] pricesLevel;

    private StatsUpgradeInfo stat;
    private bool isUpgrading;

    private void Start()
    {
        stat = StatsManager.instance.GetStats(statName);

        itemNameText.text = itemName;

        buyButton.onClick.AddListener(BuyUpgrade);

        UpdateItemDisplay();

    }

    public void BuyUpgrade()
    {
        if (stat.level == pricesLevel.Length)
        {
            DialogueManager.instance.ShowMessage($"{statName} has maximum upgrade.");
            return;
        }

        if (isUpgrading)
        {
            DialogueManager.instance.ShowMessage($"{statName} is currently upgrading.");
            return;
        }

        if(StatsManager.instance.money >= pricesLevel[stat.level])
        {
            DialogueManager.instance.ShowDialogue($"Are you sure you want to upgrade {stat.name}", () =>
            {
                StatsManager.instance.AddMoney(-pricesLevel[stat.level]);

                StatsManager.instance.statsTimer.Add(statName, DateTime.Now.AddMinutes(StatsManager.instance.GetUpgradeTime(statName)[stat.level]));

                StartCoroutine(DoUpgrade());
            });

            Debug.Log("Upgrading" + statName);
        }
        else
        {
            DialogueManager.instance.ShowMessage($"You don't have enough money to upgrade {stat.name}!");
        }
    }

    public void UpdateItemDisplay()
    {
        UpdateMoney.instance.UpdateMoneyDisplay();

        stat = StatsManager.instance.GetStats(statName);

        itemLevelBar.value = stat.level;

        if(stat.level == pricesLevel.Length)
        {
            buyText.text = "MAX";
            return;               
        }

        buyText.text = pricesLevel[stat.level].ToString();

        CheckForUpgradeStatus();
    }

    public void CheckForUpgradeStatus()
    {
        if (StatsManager.instance.statsTimer.ContainsKey(statName))
        {
            if(DateTime.Now < StatsManager.instance.statsTimer[statName])
            {
                StartCoroutine(DoUpgrade());
            }
            else
            {
                IncreaseStat();
            }
        }
    }

    private IEnumerator DoUpgrade()
    {
        isUpgrading = true;

        TimeSpan timeRemaining = StatsManager.instance.statsTimer[statName] - DateTime.Now;

        while(timeRemaining.TotalSeconds > 0f)
        {
            timeRemaining = StatsManager.instance.statsTimer[statName] - DateTime.Now;
            buyText.text =  string.Format("{0:00}:{1:00}", timeRemaining.Minutes, timeRemaining.Seconds);
           yield return null;
        }

        isUpgrading = false;

        IncreaseStat();
    }

    private void IncreaseStat()
    {
        stat.level++;

        if (isUpgrading)
        {
            StopAllCoroutines();
            isUpgrading = false;
        }

        if (stat.level == pricesLevel.Length)
        {
            buyText.text = "MAX";
            itemLevelBar.value = stat.level;
            return;
        }

        buyText.text = pricesLevel[stat.level].ToString();
        itemLevelBar.value = stat.level;      

        StatsManager.instance.statsTimer.Remove(statName);

        DialogueManager.instance.ShowMessage($"Fininsh upgraiding {statName}");

    }
}
