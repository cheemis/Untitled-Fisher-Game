using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoat : MonoBehaviour
{
    //this class is the boat that the player drives


    // speed/control variables
    [SerializeField]
    private float acceleration = 50f;
    [SerializeField]
    private float maxSpeed = 1000f;
    //[SerializeField]
    //private float dragSpeed;

    [Space(30)]

    //rotation variables
    [SerializeField]
    private float rotationSpeed = 50f;
    [SerializeField]
    private float maxRotationSpeed = 10f;
    private float currentRotationSpeed = 0;

    [Space(30)]

    //control variables
    [SerializeField]
    private float slowDownSpeed = 1;
    private bool canControlBoat = true;

    [Space(30)]

    //component variables
    private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        //rotate boat left/right
        RotatePlayer(Input.GetAxis("Horizontal"));


        //move boat forward/backwards input
        MovePlayer(Input.GetAxis("Vertical"));

        //Debug.Log("current velocity: " + rb.velocity.magnitude);
    }
    
    private void MovePlayer(float direction)
    {
        float x = 0;
        float y = 0;
        float z = 0;
        Vector3 newVelocity = Vector3.zero;

        //apply the direction held
        rb.AddForce(transform.forward * direction * acceleration * Time.deltaTime, ForceMode.Force);
        newVelocity = rb.velocity;

        //clamp the direction so it doesn't go too fast
        x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
        y = Mathf.Clamp(newVelocity.y, -maxSpeed, maxSpeed);
        z = Mathf.Clamp(newVelocity.z, -maxSpeed, maxSpeed);

        //apply new velocity
        rb.velocity = new Vector3(x,y,z);
    }


    private void RotatePlayer(float rotationDirection)
    {
        Quaternion currentRotation = rb.rotation;

        //gradually increase turning speed as you speed up ==== MAGIC NUMBERS ====
        float currentSpeed = Mathf.Clamp(rb.velocity.magnitude / maxSpeed, .25f, 1);
        currentRotationSpeed += rotationSpeed * rotationDirection * currentSpeed;
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, -maxRotationSpeed, maxRotationSpeed) * Time.deltaTime;

        rb.MoveRotation(currentRotation * Quaternion.Euler(0, currentRotationSpeed, 0));
    }

    IEnumerator SlowDownBoat()
    {
        canControlBoat = false;
        while(rb.velocity.magnitude > .1)
        {
            rb.velocity -= slowDownSpeed;
            yield return null;
        }
        yield return null;


    }


}
