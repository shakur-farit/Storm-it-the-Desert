using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private Vector3 movement, target;
    private Magnet magnet;
    private Quaternion deltaRotation;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        movement = transform.position;
        movement += Random.insideUnitSphere * speed;
        movement.y = 0;
    }

    private void Start()
    {
        magnet = FindObjectOfType<Magnet>();

        deltaRotation = Quaternion.Euler(new Vector3(Random.Range(10f, 500f), 0, 0) * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        target = Vector3.Lerp(transform.position, movement, 1f * Time.fixedDeltaTime);

        if((magnet.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(magnet.magnetRange, 2f))
        {
            target = Vector3.Lerp(transform.position, magnet.transform.position, magnet.magnetPower * Time.fixedDeltaTime);
        }

        rb.MovePosition(target);       
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
