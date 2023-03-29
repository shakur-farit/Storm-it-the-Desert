using UnityEngine;
using UnityEngine.Events;

public class CoinPickUp : MonoBehaviour
{
    public UnityEvent onPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            PoolingManager.instance.ReturnObject(other.gameObject);
            onPickedUp.Invoke();

            //TO-DO add some counter for coins.
        }
    }
}
