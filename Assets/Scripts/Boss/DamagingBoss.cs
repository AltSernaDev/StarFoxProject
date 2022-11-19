using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingBoss : MonoBehaviour,HitInterface
{
    [SerializeField] private Material normal, hited;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void hit(float dmgPoints)
    {
        BossManager.instance.TakeDamage(dmgPoints);
        StartCoroutine(changeColor());
    }

    IEnumerator changeColor()
    {
        _meshRenderer.material = hited;
        yield return new WaitForSeconds(0.05f);
        _meshRenderer.material = normal;
    }
}
