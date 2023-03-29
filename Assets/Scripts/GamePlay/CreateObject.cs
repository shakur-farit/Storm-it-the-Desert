using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public CreatItems[] creatItems;
    public void Create()
    {
        for (int i = 0; i < creatItems.Length; i++)
        {
            creatItems[i].position = transform.position;
            creatItems[i].position.y = 0f;
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);

            for (int j = 0; j < creatItems[i].creatAmount; j++)
            {
                GameObject temp = PoolingManager.instance.UseObject(creatItems[i].objectToCreate, creatItems[i].position, Quaternion.identity);

                if (creatItems[i].autoDestroy)
                    PoolingManager.instance.ReturnObject(temp, creatItems[i].tiemToDestroy);
            }
        }
    }
}

[System.Serializable]
public class CreatItems
{
    public GameObject objectToCreate;
    public int creatAmount = 1;

    [Header("Auto-Destroy Properties")]
    public bool autoDestroy;
    public float tiemToDestroy;

    [HideInInspector]
    public Vector3 position;
}
