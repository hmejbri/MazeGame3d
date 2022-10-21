using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float smoothFactor = .5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = target.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor * Time.deltaTime);
    }
}
