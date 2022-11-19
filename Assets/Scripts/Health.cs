using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPlayer = 100;
    public static Health hpCode;

    private void Start()
    {
        hpCode = this;
    }

    //private Collider collRef;
    

    public void TakeDamage(float dmgPoint)
    {
        healthPlayer -= dmgPoint;
    }
}
