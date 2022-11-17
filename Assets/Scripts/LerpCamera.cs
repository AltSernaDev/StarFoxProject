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
    private Vector2 target;
    
    
    private void FixedUpdate()
    {
        target = new Vector3(airwing.position.x, airwing.position.y);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime, maxSpeed);
    }
}
