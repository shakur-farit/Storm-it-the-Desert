using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : BulletMove
{
    public float rotateSpeed = 2f, followDuration = 3f;
    public bool isPlayer;

    private Transform target;
    private WaitForSeconds physicsTimeStep;

    private void Awake()
    {
        myRg = GetComponent<Rigidbody>();
        physicsTimeStep = new WaitForSeconds(Time.fixedDeltaTime);
    }

    private void Start()
    {
        if (!isPlayer)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
                target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartFollow(followDuration));

        if (isPlayer)
        {
            if (GameObject.FindGameObjectWithTag("Enemy"))
                target = FindEnemy();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartFollow(float followDuration)
    {
        while(followDuration > 0f)
        {
            followDuration -= Time.fixedDeltaTime;

            if(target != null)
            {
                Vector3 dir = target.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.fixedDeltaTime);
            }

            myRg.velocity = transform.forward * speed;

            yield return physicsTimeStep;
        }      
    }

    private Transform FindEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Array.Sort(enemies, delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position).
            CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });

        //if (Application.isEditor)
        //{
        //    for (int i = 0; i < enemies.Length; i++)
        //    {
        //        print(Vector3.Distance(transform.position, enemies[i].transform.position));
        //    }
        //}

        return enemies[0].transform;
    }

}
