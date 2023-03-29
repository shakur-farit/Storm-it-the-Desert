using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldDuration;
    public GameObject hitEffect;

    private WaitForSeconds shieldDelay;

    private Collider coll;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        shieldDelay = new WaitForSeconds(shieldDuration);

        coll = GetComponent<Collider>();
        coll.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShieldUp();
        }
    }


    public void ShieldUp()
    {
        StartCoroutine(EngageShield());
    }

    private IEnumerator EngageShield()
    {
        coll.enabled = true;
        float inAnimDuration = 0.5f;
        float outAnimDuration = 0.5f;

        while (inAnimDuration > 0f)
        {
            inAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.1f);
            yield return null;
        }

        yield return shieldDelay;

        while (outAnimDuration > 0f)
        {
            outAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.1f);
            yield return null;
        }

        transform.localScale = Vector3.zero;
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            if (hitEffect != null)
            {
                Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
                Vector3 direction = triggerPosition - transform.position;

                GameObject fx = PoolingManager.instance.UseObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

                PoolingManager.instance.ReturnObject(fx, 1f);
            }

            HealthSystem health = other.GetComponent<HealthSystem>();
            DamagePlayer damagePlayer = other.GetComponent<DamagePlayer>();

            if(health != null && damagePlayer != null)
            {
                health.TakeDamage(damagePlayer.takemDamageValue, other);
            } 
            else if(!other.CompareTag("Enemy"))
            {
                PoolingManager.instance.ReturnObject(other.gameObject);
            }
        }
    }
}
