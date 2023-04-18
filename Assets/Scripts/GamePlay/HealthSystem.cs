using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject hitEffect, healthBar;
    public bool isEnemy = true;

    public int minScore = 20, maxScore = 50;

    private string tagName = "Bullet";
    private float currentHealth;
    private DeathSystem deathSystem;


    private void OnEnable()
    {
        if (isEnemy)
            tagName = "Bullet";
        else
        {
            tagName = "EnemyBullet";

            maxHealth = StatsManager.instance.GetStatsValue("Health", StatsManager.instance.healthUpgradeList);
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (isEnemy)
            LevelManager.instance.RegisterEnemy();
        deathSystem = GetComponent<DeathSystem>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            if (!isEnemy && LevelManager.instance.medals.untouched)
                LevelManager.instance.PlayerHit();

            float damage =  float.Parse(other.name);
            TakeDamage(damage, other);

            PoolingManager.instance.ReturnObject(other.gameObject);
        }
    }

    public void TakeDamage(float damage, Collider other)
    {
        if (hitEffect != null)
        {
            Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.instance.UseObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

            PoolingManager.instance.ReturnObject(fx, 1f);
        }

        currentHealth -= damage;
        CheckHealth();
        UpdateUI();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0 && this.enabled == true)
        {
            if (healthBar != null)
                healthBar.transform.parent.gameObject.SetActive(false);
            

            if (deathSystem != null)
                deathSystem.Death();

            if (isEnemy)
            {
                gameObject.tag = "Untagged";
                LevelManager.instance.AddEnemyKill(Random.Range(minScore,maxScore+1));
            }
        }
    }

    private void UpdateUI()
    {
        if (healthBar != null && this.enabled == true)
        {
            Vector3 scale = Vector3.one;
            float value = currentHealth / maxHealth;
            scale.x = value;
            healthBar.transform.localScale = scale;
        }
    }
}
