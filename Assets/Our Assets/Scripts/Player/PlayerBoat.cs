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
    [SerializeField]
    private float rotationSpeed = 50f;


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
        //RotatePlayer(Input.GetAxis("Horizontal"));

        //move boat forward/backwards input
        MovePlayer(Input.GetAxis("Vertical"));
    }
    
    private void MovePlayer(float direction)
    {
        if(rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * direction * acceleration * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    /*
    private void RotatePlayer(float rotationDirection)
    {
        rb.MoveRotation()
    }
    */


}
