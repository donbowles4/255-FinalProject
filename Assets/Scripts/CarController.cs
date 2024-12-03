using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 15f;
    public float steering = 40f;
    public float brakeForce = 20f;
    public float maxSpeed = 25f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // user input for acceleration and steering
        float moveInput = Input.GetAxis("Vertical");
        float steerInput = Input.GetAxis("Horizontal");

        // get movement force and apply to car
        if (moveInput > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveInput * acceleration);
        }

        // steering
        float steeringAngle = steerInput * steering * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, steeringAngle, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // brake
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-rb.velocity.normalized * brakeForce);
        }
    }
}
