using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathSystem : MonoBehaviour
{
    public bool isDestructable = false, backToPool = false;

    public float destroyAfter;
    public CreateObject[] spawnObjects;
    public UnityEvent onDeathEvent;

    private Collider[] colliders;

    private void Start()
    {
        colliders = GetComponents<Collider>();
    }

    public void Death()
    {
        for(int i = 0; i < spawnObjects.Length; i++)
        {
            spawnObjects[i].Create();
        }

        onDeathEvent.Invoke();

        if (isDestructable)
        {
            if (backToPool)
                PoolingManager.instance.ReturnObject(gameObject, destroyAfter);
            else
                Destroy(gameObject, destroyAfter);
        }


        for (int i = 0; i < colliders.Length; i++)
        {
                colliders[i].enabled = false;
        }
    }
}
