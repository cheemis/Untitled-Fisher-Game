using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{

    //components
    private Transform hookContainer;
    



    // Start is called before the first frame update
    void Start()
    {
        hookContainer = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void GetWorldPositionFromMouse()
    {

    }

    private void AimHook(Vector3 worldMousePosition)
    {
        Vector3 lookAtTarget = new Vector3(worldMousePosition.x,
                                           hookContainer.position.y,
                                           worldMousePosition.z);
        hookContainer.LookAt(lookAtTarget);
    }
}
