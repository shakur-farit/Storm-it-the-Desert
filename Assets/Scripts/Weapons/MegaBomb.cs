using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBomb : MonoBehaviour
{
    public float radius = 2f, damage = 2f;
    public ParticleSystem mbFX;

    private void Start()
    {
        if (mbFX != null)
        {
            var partMain = mbFX.main;
            partMain.startSize = radius * partMain.startSize.constant;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DeployBomb();
        }
    }


    public void DeployBomb()
    {
        Debug.Log("Bdshhhhh!");

        mbFX.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthSystem health = colliders[i].GetComponent<HealthSystem>();

            if(health != null && colliders[i].CompareTag("Enemy"))
            {
                health.TakeDamage(damage, colliders[i]);

                Debug.Log(colliders[i].name + " was fckn destroy by devastating mega blast. BOOOOOOOOOM!");
            }
        }
    }
}
