using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public float rotateSpeed = 2f;

    private Transform target;
    private Vector3 lookDirection;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(target != null)
        {
            lookDirection = target.position - transform.position;
            lookDirection.y = 0f;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotateSpeed * Time.deltaTime);
        }
    }
}
