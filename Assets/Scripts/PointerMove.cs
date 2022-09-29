using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    private Vector3 pointerMoVector3;
    [SerializeField] private float speedPointMove=5;
    
    void Update()
    {
        pointerMoVector3 = new Vector3(Input.GetAxis("Horizontal") * speedPointMove,
            -Input.GetAxis("Vertical") * speedPointMove, 0);
    }

    private void FixedUpdate()
    {
        transform.position = pointerMoVector3;
    }
}
