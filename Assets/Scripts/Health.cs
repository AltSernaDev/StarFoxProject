using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPlayer = 100;
    
    //private Collider collRef;
    

    public void TakeDamage(float dmgPoint)
    {
        healthPlayer -= dmgPoint;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
