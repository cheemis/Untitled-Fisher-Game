using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignSpriteToCamera : MonoBehaviour
{
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        Vector3 lookAtDir = new Vector3(cameraPos.x + offset.x,
                                        transform.position.y + offset.y,
                                        cameraPos.z + offset.z);

        transform.LookAt(lookAtDir);
    }
}
