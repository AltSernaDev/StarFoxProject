using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPlayer = 100;
    //private Collider collRef;
    

    public void TakeDamage(int dmgPoint)
    {
        healthPlayer -= dmgPoint;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
