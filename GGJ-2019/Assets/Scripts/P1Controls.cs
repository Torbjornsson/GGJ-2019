using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controls : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal1");
        float moveV = Input.GetAxis("Vertical1");

        Vector3 movement = new Vector3 (moveH, 0.0f, moveV);

        rb.AddForce(movement * speed);
    }
}
