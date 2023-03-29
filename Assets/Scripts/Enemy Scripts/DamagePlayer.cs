using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int dealDamageValue, takemDamageValue;

    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed)
            return;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(dealDamageValue, other);
            GetComponent<HealthSystem>().TakeDamage(takemDamageValue, other);

            destroyed = true;
        }
    }
}
