using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int dealDamageValue, takenDamageValue;

    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed)
            return;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(dealDamageValue, other);
            GetComponent<HealthSystem>().TakeDamage(takenDamageValue, other);

            destroyed = true;
        }
    }
}
