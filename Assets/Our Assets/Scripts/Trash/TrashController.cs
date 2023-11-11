using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashController : MonoBehaviour
{
    public enum TrashType { Small, Medium, Large };
    public TrashType trashType = TrashType.Medium;
    // Start is called before the first frame update
    void Start()
    {
        switch(trashType)
        {
            case TrashType.Small:
                transform.localScale = Vector3.one;
                break;
            case TrashType.Medium:
                transform.localScale = new Vector3(2,2,2);
                break;
            case TrashType.Large:
                transform.localScale = new Vector3(3,3,3);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
