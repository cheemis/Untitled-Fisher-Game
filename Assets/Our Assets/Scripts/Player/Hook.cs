using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    //Hook Container
    private GameObject hookContainerGameObject;
    private PlayerHook hookContainer;
    private Transform latchingPoint;
    private Transform HoldingPosition;

    [Space(30)]

    //catching variables
    private Vector3 targetPosition = Vector3.zero;
    private Collectable caughtCollectable = null;

    enum CastingStates
    {
        casting,
        idling,
        reeling
    }
    private CastingStates castingState = CastingStates.casting;

    [Space(30)]

    //casting variables
    [SerializeField]
    private float castingSpeed = 20f;

    [Space(30)]
    [SerializeField]
    private float idleTime = .5f;
    private float timeToIdle = 0f;

    [Space(30)]

    //reeling variables
    [SerializeField]
    private float reelSpeed = 10f;


    public void Initialize(GameObject hookContainerGameObject, Transform latchingPoint, Transform holdingPosition)
    {
        this.hookContainerGameObject = hookContainerGameObject;
        this.hookContainer = hookContainerGameObject.GetComponent<PlayerHook>();
        this.latchingPoint = latchingPoint;
        this.HoldingPosition = holdingPosition;
        this.gameObject.SetActive(false);
    }


    private void Update()
    {
        switch (castingState)
        {
            //casting and trying to hit sometime
            case CastingStates.casting:

                SendOutHook();
                break;

            //waiting once it hits something to start reeling back
            case CastingStates.idling:
                if (timeToIdle < Time.time) castingState = CastingStates.reeling;
                break;
            
            //reeling back towards the boat
            case CastingStates.reeling:

                ReelInHook();
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            /*
            currentlyCaught = other.gameObject;
            currentlyCaught.GetComponent<Collectable>().Bind(this.gameObject);
            */

        }
    }

    // ===================================== //
    // ====== START CASTING FUNCTIONS ====== //
    // ===================================== //


    public void StartCasting(Vector3 targetPosition)
    {

        //reset variables
        caughtCollectable = null;
        castingState = CastingStates.casting;

        //set casting locations
        transform.position = latchingPoint.position;
        this.targetPosition = targetPosition;


        Debug.Log("started casting hook");
    }





    // ===================================== //
    // ========= CASTING FUNCTIONS ========= //
    // ===================================== //

    private void SendOutHook()
    {
        AudioManager.Instance.Play("ShootFishPole");
        //give up condition
        if (targetPosition == Vector3.zero || Vector3.Distance(transform.position, targetPosition) < .25f)
        {
            //prepare for idling
            timeToIdle = Time.time + idleTime;
            castingState = CastingStates.idling;

            Debug.Log("stopped sending hook");
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, castingSpeed * Time.deltaTime);

    }

    private void ReelInHook()
    {
        AudioManager.Instance.Play("ReelFishPole");
        transform.position = Vector3.Lerp(transform.position, latchingPoint.transform.position, reelSpeed * Time.deltaTime);

        //check if returned home
        if (Vector3.Distance(transform.position, latchingPoint.transform.position) < 1f)
        {
            StopCasting();
        }
    }

    private void StopCasting()
    {
        if (caughtCollectable != null)
        {
            caughtCollectable.UpdateLockedTarget(HoldingPosition);
            hookContainer.SetCurrentCollectable(caughtCollectable);
        }


            this.gameObject.SetActive(false);
    }

    public void SetCurrentHookTarget(Collectable newHookTarget)
    {
        if (newHookTarget is FishCollectable)
        {
            AudioManager.Instance.Play("CollectFish");
        }
        else if (newHookTarget is TrashCollectable)
        {
            AudioManager.Instance.Play("CollectTrash");
        }
        caughtCollectable = newHookTarget;
    }

    public bool HasCaughtFish()
    {
        return caughtCollectable != null;
    }
}
