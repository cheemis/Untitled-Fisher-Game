using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHook : MonoBehaviour
{
    //children
    private Transform arrowContainer;
    private Transform latchingPoint;
    [SerializeField]
    private Image greenArrow;
    [SerializeField]
    private Transform farthestCastingPoint;

    //collectable variable
    
    private Transform boatHoldingPosition;
    [SerializeField]
    private Collectable currentCollectable;

    [Space(30)]

    //arrow variables
    [SerializeField]
    private LayerMask arrowMask;

    [Space(30)]

    //shooting hook variables
    [SerializeField]
    private float hookFillSpeed = .75f;
    [SerializeField]
    private float hookDecreaseSpeed = 1.5f;

    [Space(30)]

    //hook variables
    [SerializeField]
    private GameObject hookPrefab;
    private GameObject hookGameObject;
    private Hook hook;

    //managing variables
    [SerializeField]
    private bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        latchingPoint = transform.GetChild(0);
        arrowContainer = transform.GetChild(1);
        boatHoldingPosition = transform.GetChild(2);

        //intialize a hook
        hookGameObject = Instantiate(hookPrefab);
        hook = hookGameObject.GetComponent<Hook>();
        hook.Initialize(this.gameObject, latchingPoint, boatHoldingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            DisplayHookArrow();
            PowerUpHook();
        }
    }



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
        arrowContainer.gameObject.SetActive(false);
    }



    // ===================================== //
    // ====== DISPLAY ARROW FUNCTIOMS ====== //
    // ===================================== //

    private void DisplayHookArrow()
    {
        Vector3 mousePosition = GetWorldPositionFromMouse();
        AimHook(mousePosition);
    }

    private Vector3 GetWorldPositionFromMouse()
    {
        //find world position where mouse hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, arrowMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void AimHook(Vector3 worldMousePosition)
    {
        if (worldMousePosition != Vector3.zero)
        {
            Vector3 lookAtTarget = new Vector3(worldMousePosition.x,
                                           arrowContainer.position.y,
                                           worldMousePosition.z);
            arrowContainer.LookAt(lookAtTarget);
        }
        else // look forward
        {
            arrowContainer.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    // ===================================== //
    // ====== SHOOTING HOOK FUNCTIONS ====== //
    // ===================================== //

    private void PowerUpHook()
    {
        float newFillAmmount = 0;

        //if the arrow doesnt exit or already casting or already holding a collectable
        if (greenArrow == null || hookGameObject.activeSelf || currentCollectable != null) return;

        newFillAmmount = greenArrow.fillAmount;

        if(Input.GetMouseButtonUp(0)) //let go
        {
            CastHook();
            newFillAmmount = 0;
        }
        else if (Input.GetMouseButton(0)) //holding mouse
        {
            newFillAmmount += hookFillSpeed * Time.deltaTime;
        }

        greenArrow.fillAmount = Mathf.Clamp(newFillAmmount, 0, 1);
    }

    private void CastHook()
    {
        if(!hookGameObject.activeSelf)
        {
            hookGameObject.SetActive(true);
            hook.StartCasting(GetHooksTargetPosition());
        }
    }

    private Vector3 GetHooksTargetPosition()
    {
        return transform.position + (farthestCastingPoint.position - transform.position) * greenArrow.fillAmount;
    }

    public void SetCurrentCollectable(Collectable caughtCollectable)
    {
        currentCollectable = caughtCollectable;
        FishingGameManager.OnCaughtSomething();
    }

    public void EnterDropOff()
    {
        if (currentCollectable != null)
        {
            AudioManager.Instance.Play("ReturnFish");
            //if a fish was collected, send an event saying a fish was collected
            if (currentCollectable is FishCollectable)
            {

                FishingGameManager.OnCollectFish();
            }
            else if (currentCollectable is TrashCollectable)
            {

                FishingGameManager.OnCollectTrash();
            }


            currentCollectable.DestroyCollectable();
            currentCollectable = null;
        }
    }
}
