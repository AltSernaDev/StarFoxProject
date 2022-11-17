using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : MonoBehaviour
{
    private Vector3 velocity;
    [SerializeField] private Transform airwing;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private float maxSpeed = 100f;
    private Vector3 target;
    [SerializeField] private bool isCam;
    
    
    private void FixedUpdate()
    {
        
        //transform.position += (new Vector3(airwing.position.x, airwing.position.y, transform.position.z).normalized) * Time.deltaTime * 20;

        if (isCam)
        {
            target = new Vector3(airwing.position.x, transform.position.y * 1.5f, transform.position.z);
        }
        else
        {
            target = new Vector3(airwing.position.x, airwing.position.y,transform.position.z);
        }
        
        target = new Vector3(airwing.position.x, airwing.position.y,transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime, maxSpeed);
    }
}
