using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage;
    public float laserDuration = 3f, animSpeed = 2f;
    public ParticleSystem burstFX;
    
    private bool laserFired = false;
    private WaitForSeconds coroutineLaserDuration;

    private Collider coll;

    

    private void Start()
    {
        coroutineLaserDuration = new WaitForSeconds(laserDuration);

        coll = GetComponent<Collider>();
        coll.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && !laserFired)
        {
            StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {
        laserFired = true;

        coll.enabled = true;

        transform.localScale = Vector3.zero;

        burstFX.Play();

        while(transform.localScale.sqrMagnitude < 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, animSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localScale = Vector3.one;

        yield return coroutineLaserDuration;

        while (transform.localScale.sqrMagnitude > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, animSpeed * Time.deltaTime);
            yield return null;
        }

        burstFX.Stop();

        transform.localScale = Vector3.zero;

        laserFired = false;

        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();

            if(health != null)
            {
                health.TakeDamage(damage, other);
            }
        }
    }
}
