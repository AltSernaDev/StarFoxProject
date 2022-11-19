using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyShoot : MonoBehaviour, HitInterface
{
    [SerializeField] private float fireRate = 0.45f;
    [SerializeField] private float ratePerBurst = 0.89f;
    [SerializeField] private int ammoPerBurst;
    private LaserShoot laserShotCode;
    [SerializeField] private GameObject laserObj;
    [SerializeField] private Transform canon;
    private Transform playerPos;
    [SerializeField] private float laserSpeed = 600f;
    private float fireTime, burstTime;
    private int currentAmmo;
    [SerializeField] private float enemyDmg = 10;


    private void Start()
    {
        burstTime = ratePerBurst;
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    private void shoot()
    {
        laserShotCode = Instantiate(laserObj,null).GetComponent<LaserShoot>();
        laserShotCode.gameObject.transform.position = canon.position;
        
        Vector3 dirVector = ((playerPos.position)-canon.position).normalized;
        //Vector3 dirVector = (pointer.position-camera.position).normalized;
        laserShotCode.shooting(dirVector,laserSpeed,enemyDmg);
    }
    
    
    void Update()
    {
        if (fireTime > fireRate)
        {
            if (currentAmmo < ammoPerBurst)
            {
                shoot();
                fireTime = 0;
                currentAmmo++;
            }
            else
            {
                burstTime -= Time.deltaTime;
                if (burstTime<0)
                {
                    currentAmmo = 0;
                    burstTime = ratePerBurst;
                }
            }
        }
        fireTime += Time.deltaTime;
    }

    public void hit(float dmgPoints)
    {
        transform.DOShakePosition(0.15f, 1.5f, 12, 10, false, false, ShakeRandomnessMode.Harmonic);
        Destroy(gameObject,0.165f);
    }
}
