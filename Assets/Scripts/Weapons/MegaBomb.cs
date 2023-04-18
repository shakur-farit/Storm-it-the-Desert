using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBomb : MonoBehaviour
{
    public float radius = 2f, damage = 2f;
    public ParticleSystem mbFireFX, mbSmokeFX;

    private void Start()
    {
        if (mbFireFX == null && mbSmokeFX == null)
            return;

        var partFireMain = mbFireFX.main;
        partFireMain.startSize = radius * partFireMain.startSize.constant;
        var partSmokeMain = mbSmokeFX.main;
        partSmokeMain.startSize = radius * partSmokeMain.startSize.constant + 2;


        radius = StatsManager.instance.GetStatsValue("MegaBomb", StatsManager.instance.megaBombUpgradeList).radius;
        damage = StatsManager.instance.GetStatsValue("MegaBomb", StatsManager.instance.megaBombUpgradeList).damage;
    }

    public void DeployBomb()
    {
        mbFireFX.Play();
        gameObject.transform.DetachChildren();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthSystem health = colliders[i].GetComponent<HealthSystem>();

            if (health != null && colliders[i].CompareTag("Enemy"))
            {
                health.TakeDamage(damage, colliders[i]);
            }   
        }
    }
}
