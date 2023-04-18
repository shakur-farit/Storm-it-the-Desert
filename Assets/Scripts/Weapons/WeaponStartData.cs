using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStartData : MonoBehaviour
{
    public AutoShoot autoShoot;
    public string weaponStatName;

    void Start()
    {
        if (weaponStatName == "Machinegun")
        {
            autoShoot.SwitchProfile(StatsManager.instance.GetStatsValue(weaponStatName, StatsManager.instance.machinegunUpgradeList));
            Debug.Log("SwitchMachine");
        }
        else if (weaponStatName == "Missile")
            autoShoot.SwitchProfile(StatsManager.instance.GetStatsValue(weaponStatName, StatsManager.instance.missileUpgradeList));

        autoShoot.SetIntervalValue();
    }  
    
}
