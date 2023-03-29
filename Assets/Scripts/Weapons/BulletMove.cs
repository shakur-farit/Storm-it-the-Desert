using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    protected Rigidbody myRg;

    private void Start()
    {
        myRg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myRg.velocity = transform.forward * speed;
    }
}
