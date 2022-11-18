using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.45f;
    [SerializeField] private float ratePerBurst = 0.89f;
    [SerializeField] private shootMode _shootMode;
    [SerializeField] private int ammoPerBurst;
    private LaserShoot laserShotCode;
    private GameObject laserObj;
    [SerializeField] private Transform canon;
    private Transform playerPos;
    [SerializeField] private float laserSpeed = 600f;
    private float fireTime, burstTime;
    private int currentAmmo;


    private void shoot()
    {
        laserShotCode = Instantiate(laserObj,null).GetComponent<LaserShoot>();
        laserShotCode.gameObject.transform.position = canon.position;
        
        Vector3 dirVector = (playerPos.position-canon.position).normalized;
        //Vector3 dirVector = (pointer.position-camera.position).normalized;
        laserShotCode.shooting(dirVector,laserSpeed);
    }
    
    public enum shootMode
    {
        singleShot,
        dobleShot,
        tripleShot
    }
    
    void Update()
    {
        if (fireTime > fireRate)
        {
            if (currentAmmo > ammoPerBurst)
            {
                shoot();
                fireTime = 0;
                currentAmmo--;
            }
            else
            {
                
            }
        }
        
        fireTime += Time.deltaTime;
    }
}
