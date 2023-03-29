using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float minX, maxX, speed = 2f;

    private Vector3 pos;

    private void LateUpdate()
    {
        if (player == null)
            return;

        pos = transform.localPosition;
        pos.x = player.localPosition.x;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, speed * Time.deltaTime);
    }
}
