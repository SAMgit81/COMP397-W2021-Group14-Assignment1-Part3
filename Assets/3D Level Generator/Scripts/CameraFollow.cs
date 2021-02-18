using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Range(0, 1)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    [SerializeField] bool isSmoothing = true;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        if (isSmoothing)
        {
            //Vector3.Lerp
            desiredPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
        transform.position = desiredPosition;
    }
}