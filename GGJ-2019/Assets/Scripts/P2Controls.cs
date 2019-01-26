using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controls : MonoBehaviour
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
        float moveH = Input.GetAxis("RightHorizontal");
        float moveV = Input.GetAxis("RightVertical");

        Vector3 movement = new Vector3 (moveH, 0.0f, moveV);

        rb.MovePosition(rb.position + movement * Time.deltaTime * speed);

        Vector3 pos = rb.transform.forward;

        float dot = Vector3.Dot(pos, movement);

        float acos = (float)Math.Acos(dot);

        Vector3 cross = Vector3.Cross(pos, movement);

        rb.AddRelativeTorque(cross * torque * acos - (rb.angularVelocity / 2), ForceMode.VelocityChange);

        if (Input.GetAxis("Fire2") > 0)
            Debug.Log("P2 picks up"); 
        
    }
}
