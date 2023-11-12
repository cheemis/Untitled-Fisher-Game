using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //collectable needs
    protected int scoreAmount = 0;

    //hook variables
    protected Transform lockedTarget = null;
    protected bool hooked = false;

    private void LateUpdate()
    {
        if(hooked && lockedTarget != null)
        {
            transform.position = lockedTarget.position;
            transform.rotation = lockedTarget.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hook")
        {
            UpdateLockedTarget(other.gameObject.transform);
            other.gameObject.GetComponent<Hook>().SetCurrentHookTarget(this);
            this.GetComponent<Collider>().enabled = false;
        }
    }

    virtual public void UpdateLockedTarget(Transform newLockedTarget)
    {
        hooked = true;
        lockedTarget = newLockedTarget;
        
    }

    public void DestroyCollectable()
    {
        Destroy(this.gameObject);
    }


}
