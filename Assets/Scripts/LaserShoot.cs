using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    private Rigidbody rgLaser;
    private Vector3 directionalVector;

    void Start()
    {
        Destroy(gameObject,1);
    }

    public void shooting(Vector3 direction, float laserSpeed)
    {
        rgLaser = gameObject.GetComponent<Rigidbody>();
        rgLaser.AddForce(direction * laserSpeed, ForceMode.Force);
    }
}
