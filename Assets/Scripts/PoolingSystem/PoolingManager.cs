using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    public PoolItem[] poolItems;

    private Dictionary<int, Queue<GameObject>> poolQueue = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, bool> growAnleBool = new Dictionary<int, bool>();
    private Dictionary<int, Transform> parents = new Dictionary<int, Transform>();



    private void Awake()
    {
        instance = this;

        PoolInit();
    }



    private void PoolInit()
    {
        GameObject pooGroup = new GameObject("Pool Group");

        for(int i = 0; i < poolItems.Length; i++)
        {
            GameObject uniquePool = new GameObject(poolItems[i].poolObject.name + " Group");
            uniquePool.transform.SetParent(pooGroup.transform);

            int objId = poolItems[i].poolObject.GetInstanceID();
            poolItems[i].poolObject.SetActive(false);

            poolQueue.Add(objId, new Queue<GameObject>());
            growAnleBool.Add(objId, poolItems[i].growAble);
            parents.Add(objId, uniquePool.transform);

            for(int j = 0; j < poolItems[i].poolAmount; j++)
            {
                GameObject temp = Instantiate(poolItems[i].poolObject, uniquePool.transform);
                poolQueue[objId].Enqueue(temp);
            }
        }
    }

    public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        int objId = obj.GetInstanceID();

        GameObject temp = poolQueue[objId].Dequeue();

        if (temp.activeInHierarchy)
        {
            if (growAnleBool[objId])
            {
                poolQueue[objId].Enqueue(temp);
                temp = Instantiate(obj, parents[objId]);
                temp.transform.position = pos;
                temp.transform.rotation = rot;
                temp.SetActive(true);
            }
            else
            {
                temp = null;
            }
        }
        else
        {
            temp.transform.position = pos;
            temp.transform.rotation = rot;
            temp.SetActive(true);
        }

        poolQueue[objId].Enqueue(temp);
        return temp;
    }

    public void ReturnObject(GameObject obj, float delay = 0f)
    {
        if(delay == 0f)
        {
            obj.SetActive(false);
        }
        else
        {
            StartCoroutine(DelayReturn(obj, delay));
        }
    }

    private IEnumerator DelayReturn(GameObject obj, float delay)
    {
        while(delay > 0f)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        obj.SetActive(false);
    }
}


[System.Serializable]
public class PoolItem
{
    public GameObject poolObject; // obj of populate
    public int poolAmount; // start amount of obj
    public bool growAble; // can amoutn grow if start amout wasn't enough
}
