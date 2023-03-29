using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f, bakingValue = 90f;
    public Transform visualContainerChild;

    private Camera cam;
    private Rigidbody myRb;
    private float distance;
    private Vector3 velocity, lastPosition, rotation, touchPosition, screenToWorld;

    private void Start()
    {
        cam = Camera.main;
        myRb = GetComponent<Rigidbody>();

        distance = (cam.transform.position - transform.position).y;
    }

    private void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        Move();

        lastPosition = transform.position;
    }

    private void Move()
    {
        touchPosition = Input.mousePosition;
        touchPosition.z = distance;

        screenToWorld = cam.ScreenToWorldPoint(touchPosition);

        Vector3 movement = Vector3.Lerp(myRb.position, screenToWorld, speed * Time.fixedDeltaTime);

        myRb.MovePosition(movement);

        rotation.z = -velocity.x * bakingValue;
        myRb.MoveRotation(Quaternion.Euler(rotation));
    }
}
