using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Header("Continuous Rotation")]
    public Vector3 rotationSpeed;
    public bool endless, onStart;

    [Header("Target Rotation")]
    public Vector3 angleRotation;
    public float speed;

    private void Start()
    {
        if (onStart)
        {
            StartCoroutine(DoRotate());
        }
    }

    public void StartRotation()
    {
        StartCoroutine(DoRotate());
    }

    private IEnumerator DoRotate()
    {
        Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles + angleRotation);
        
        if (endless)
        {
            while (endless)
            {
                transform.Rotate(rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while(transform.localRotation != targetRotation)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
