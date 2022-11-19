using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    private Rigidbody rgLaser;
    private Vector3 directionalVector;
    private ParticleSystem partSyst;
    private float damage = 10;

    void Start()
    {
        partSyst = GetComponent<ParticleSystem>();
        Destroy(gameObject,2f);
    }

    public void shooting(Vector3 direction, float laserSpeed, float dmg)
    {
        damage = dmg;
        rgLaser = gameObject.GetComponent<Rigidbody>();
        rgLaser.AddForce(direction * laserSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitInterface hitCode = other.gameObject.GetComponent<HitInterface>();
        if (hitCode != null)
        {
            hitCode.hit(damage);
            Destroy(gameObject);
        }
        else
        {
            partSyst.Play();
            Destroy(gameObject,1);
        }
    }
}
