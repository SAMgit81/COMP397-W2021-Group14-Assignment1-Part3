using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarWorldSpaceController : MonoBehaviour
{
    public Transform playerCamera;

    private void Start()
    {
        playerCamera = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //billbording 
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
