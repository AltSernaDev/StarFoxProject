using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class AirwingMove : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform canon;
    private Rigidbody rgAirWing;
    private Collider _collider;
    private Vector3 vectorPointer;
    private Vector2 velocity = Vector2.zero;
    private Quaternion targetRotation;
    public bool crashed;
    [SerializeField] private float timeToRecovery = 0.89f;
    private float recoverTime;
    public static AirwingMove airWingCode;

    private void Start()
    {
        rgAirWing = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        airWingCode = this;
    }

    void Update()
    {
        targetRotation = Quaternion.LookRotation(pointer.position - transform.position);
    }
    
    private void FixedUpdate()
    {
        if (!crashed)
        {
            transform.localPosition = Vector2.SmoothDamp(gameObject.transform.position, pointer.position, ref velocity, 0.25f,100);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation*quaternion.EulerXYZ((Input.GetAxis("Vertical")/3),0,-Input.GetAxis("Horizontal")), 7 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!crashed)
        {
            crashed = true;
            transform.DOShakePosition(0.45f, 1.5f, 8,10, false,false, ShakeRandomnessMode.Harmonic);
            Debug.Log("jajaja");
            //_collider.isTrigger = true;
            Invoke("ResetCrash",0.5f);
            Destroy(collision.gameObject);
        }

    }

    void ResetCrash()
    {
        crashed = false;
    }

    public void ShakeOfHit()
    {
        
    }
}