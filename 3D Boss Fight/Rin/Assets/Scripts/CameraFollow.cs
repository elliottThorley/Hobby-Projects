using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = playerTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }
}
