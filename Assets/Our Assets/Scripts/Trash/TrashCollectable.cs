using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashCollectable : Collectable
{
    public GameObject visual;
    public enum TrashType { Small, Medium, Large };
    public TrashType trashType = TrashType.Medium;
    public int weight;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
 
        switch(trashType)
        {
            case TrashType.Small:
                weight = 60;
                break;
            case TrashType.Medium:
                weight = 40;
                break;
            case TrashType.Large:
                weight = 30;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowTrash(float force)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.right * 100, ForceMode.Impulse);
        Vector3 direction = (TrashManager.Instance.pond.position - transform.position).normalized;
        rb.velocity = direction * force;
    }

    public override void UpdateLockedTarget(Transform newLockedTarget)
    {
        if (hooked)
        {
            //AudioManager.Instance.Play("CollectTrash");
        }
        base.UpdateLockedTarget(newLockedTarget);
        transform.localScale *= 0.5f;
        visual.transform.rotation = Quaternion.identity;

    }
}
