using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AirwingMove : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform canon;
    private Rigidbody rgAirWing;
    private Vector3 vectorPointer;
    private Vector2 velocity = Vector2.zero;
    private Quaternion targetRotation;

    void Update()
    {
        targetRotation = Quaternion.LookRotation(pointer.position - transform.position);
    }
    
    private void FixedUpdate()
    {
        transform.localPosition = Vector2.SmoothDamp(gameObject.transform.position, pointer.position, ref velocity, 0.25f,100);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation*quaternion.EulerXYZ((Input.GetAxis("Vertical")/3),0,-Input.GetAxis("Horizontal")), 7 * Time.deltaTime);
    }
}