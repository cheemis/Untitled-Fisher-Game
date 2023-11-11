using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashCollectable : Collectable
{
    public enum TrashType { Small, Medium, Large };
    public TrashType trashType = TrashType.Medium;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowTrash()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.right * 100, ForceMode.Impulse);
        Vector3 direction = (TrashManager.Instance.pond.position - transform.position).normalized;
        rb.velocity = direction * 50;
    }
}
