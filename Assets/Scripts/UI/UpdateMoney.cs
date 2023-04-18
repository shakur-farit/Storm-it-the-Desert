using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMoney : MonoBehaviour
{
    public static UpdateMoney instance;

    public TMP_Text moneyDisplay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisplayMoney(StatsManager.instance.money);
    }

    public void UpdateMoneyDisplay()
    {
        DisplayMoney(StatsManager.instance.money);
    }

    public void DisplayMoney(int value)
    {
        moneyDisplay.text = "$ " + value.ToString();
    }
}
