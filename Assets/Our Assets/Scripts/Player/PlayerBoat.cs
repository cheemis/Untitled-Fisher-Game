using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
public class PlayerBoat : MonoBehaviour
{
    //this class is the boat that the player drives


    // speed/control variables
    [SerializeField]
    private float acceleration = 50f;
    [SerializeField]
    private float maxSpeed = 1000f;

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
    Animator anim;

    [Space(30)]

    //other variables
    [SerializeField]
    private PlayerHook hook;

    //managing variables
    private bool gameOver = false;

    //motor boat sfx
    EventInstance motorBoatSFX;
    //DropOff SFX Emitter
    StudioEventEmitter emitter;

    // ================================== //
    // ======= BUILT-IN FUNCTIONS ======= //
    // ================================== //

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hook = GetComponentInChildren<PlayerHook>();
        anim = GetComponent<Animator>();
        motorBoatSFX = FMODAudioManager.Instance.CreateFMODEventInstance(FMODEvents.Instance.motorBoat);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if (canControlBoat)
            {
                ReadInput();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Drop Off")
        {
            hook.EnterDropOff();
            //emitter = FMODAudioManager.Instance.InitializeFMODEventEmitter(FMODEvents.Instance.houseBouncing, other.gameObject);
            //emitter.Play();

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Drop Off")
    //    {
    //        emitter.Stop();
    //    }
    //}

    // ===================================== //
    // ===== LISTENING/EVENT FUNCTIONS ===== //
    // ===================================== //

    private void OnEnable()
    {
        FishingGameManager.gameOver += EndGame;
    }

    private void OnDisable()
    {
        FishingGameManager.gameOver -= EndGame;
    }

    private void EndGame()
    {
        gameOver = true;
        anim.SetFloat("speed", 0);
        anim.SetFloat("turning", 0);
    }



    // ===================================== //
    // ======== TRAVERSAL FUNCTIONS ======== //
    // ===================================== //

    private void ReadInput()
    {
        //move boat forward/backwards input
        float verticalSpeed = Input.GetAxis("Vertical");
        anim.SetFloat("speed", verticalSpeed);
        MovePlayer(verticalSpeed);

        //rotate boat left/right
        float horizontalSpeed = Input.GetAxis("Horizontal");
        anim.SetFloat("turning", horizontalSpeed);
        RotatePlayer(horizontalSpeed, Mathf.Sign(verticalSpeed));
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
        if (direction!=0.0f)
        {
            PLAYBACK_STATE playbackState;
            motorBoatSFX.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                motorBoatSFX.start();
            }
        }
        else
        {
            motorBoatSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //Debug.Log("Boat Stopped");
        }

    }

    private void RotatePlayer(float rotationDirection, float direction)
    {
        Quaternion currentRotation = rb.rotation;

        //gradually increase turning speed as you speed up ==== MAGIC NUMBERS ====
        float currentSpeed = Mathf.Clamp(rb.velocity.magnitude / maxSpeed, .25f, 1);
        currentRotationSpeed += rotationSpeed * rotationDirection * currentSpeed;
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, -maxRotationSpeed, maxRotationSpeed) * direction * Time.deltaTime;

        rb.MoveRotation(currentRotation * Quaternion.Euler(0, currentRotationSpeed, 0));
    }



    // ===================================== //
    // ======= CONTROLLING FUNCTIONS ======= //
    // ===================================== //

    public void StopBoat()
    {
        if(canControlBoat)
        {
            StartCoroutine(SlowDownBoat());

            //AudioManager.Instance.Stop("MotorBoat");
        }

    }

    public void StartBoat()
    {
        canControlBoat = true;
        //AudioManager.Instance.Play("MotorBoat");



    }

    public void StartRockingBoat()
    {

    }

    public void StopRockingBoat()
    {

    }



    // ===================================== //
    // ============ COROUTINES ============= //
    // ===================================== //

    //This Coroutine slows down the player to zero
    IEnumerator SlowDownBoat()
    {
        canControlBoat = false;
        Vector3 originalVelocity = rb.velocity;

        while(rb.velocity.magnitude > .1)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, slowDownSpeed * Time.deltaTime);
            yield return null;
        }
        rb.velocity = Vector3.zero;

        yield return null;
        StartBoat();
    }
}
