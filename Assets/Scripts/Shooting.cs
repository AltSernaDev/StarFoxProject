using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject laserObj;
    private LaserShoot laserShotCode;
    private bool canShoot = true;
    [SerializeField] private float fireRate = 0.3f;
    private float fireTime;
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform canon;
    [SerializeField] private float laserSpeed = 100;
    [SerializeField] private Transform camera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canShoot)
        {
            shoot();
            canShoot = false;
        }

        if (!canShoot)
        {
            fireTime += Time.deltaTime;
            if (fireTime >= fireRate)
            {
                fireTime = 0;
                canShoot = true;
            }
        }
    }
    
    private void shoot()
    {
        laserShotCode = Instantiate(laserObj,null).GetComponent<LaserShoot>();
        laserShotCode.gameObject.transform.position = canon.position;
        
        Vector3 dirVector = (pointer.position-canon.position).normalized;
        //Vector3 dirVector = (pointer.position-camera.position).normalized;
        laserShotCode.shooting(dirVector,laserSpeed);
    }
}
