using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    private Rigidbody rg;
    private Vector3 pointerMoVector3;
    private float x, y;
    [SerializeField] private float speedPointMove = 5;

    private void Start()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = -Input.GetAxisRaw("Vertical");
    }



    private void FixedUpdate()
    {
        transform.position += (new Vector3(x, y, 0).normalized) * Time.deltaTime * speedPointMove;
        
        
        var pos = transform.position;
        pos.x =  Mathf.Clamp(transform.position.x, -14f, 14f);
        pos.y = Mathf.Clamp(transform.position.y, -10f, 10f);
        transform.position = pos;
    }
}
