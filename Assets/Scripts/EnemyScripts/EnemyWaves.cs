using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public int numWaves = 1;
    public float intervalBetweenEnemy = 0.5f, removeAfter = 2f;
    public List<GameObject> childs = new List<GameObject> ();

    private GameObject mainChild;
    private WaitForSeconds interval, disableAfter;

    private void Start()
    {
        interval = new WaitForSeconds (intervalBetweenEnemy);
        disableAfter = new WaitForSeconds (removeAfter);
        Initialize();

        StartCoroutine(StartWaves());
        StartCoroutine(CheckCombo());
    }

    private void Initialize()
    {
        mainChild = transform.GetChild(0).gameObject;
        Vector3 pos = mainChild.transform.position;
        mainChild.SetActive(false);
        childs.Add(mainChild);

        for(int i = 0; i < numWaves; i++)
        {
            GameObject temp = Instantiate(mainChild, pos, mainChild.transform.rotation);
            childs.Add(temp);
            childs[i].transform.SetParent(transform);
            childs[i].SetActive(false);
        }
    }

    private IEnumerator StartWaves()
    {
        int i = 0;
        while (i < numWaves)
        {
            childs[i].SetActive(true);
            StartCoroutine(DisableChild(childs[i]));
            i++;
            yield return interval;
        }
    }

    private IEnumerator DisableChild(GameObject obj)
    {
        yield return disableAfter;

        if(obj != null)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator CheckCombo()
    {
        yield return disableAfter;

        if(transform.childCount < 1)
        {
            print("K-k-k-kombo!");
        } 
        else
        {
            print("Why are you so LOSER?");
        }
    }
}
