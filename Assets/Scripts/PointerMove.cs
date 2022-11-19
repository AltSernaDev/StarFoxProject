using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointerMove : MonoBehaviour
{
    private Rigidbody rg;
    private Vector3 pointerMoVector3;
    private float x, y;
    [SerializeField] private float clampX = 14f;
    [SerializeField] private float clampY = 9f;
    [SerializeField] private float speedPointMove = 5;

    [NonSerialized] public Vector3 modifier;

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
        if (!AirwingMove.airWingCode.crashed)
        {
            var pos = transform.position;
            pos.x =  Mathf.Clamp(transform.position.x, -clampX, clampX);
            pos.y = Mathf.Clamp(transform.position.y, -clampY, clampY);
            transform.position = pos;
            transform.position += (new Vector3(x, y, 0).normalized + new Vector3(modifier.x, modifier.y, 0)) * Time.deltaTime * speedPointMove;
            modifier = Vector3.zero;
        }
        else
        {
            transform.position +=
                new Vector3(Random.Range(0, 1), Random.Range(0, 1), 0) * Time.deltaTime * (speedPointMove*2);
        }
    }
}
