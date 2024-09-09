using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XGravity : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 position = new Vector3(-9.8f, 0, 0);
        rb.AddForce(position);
        

    }
}
