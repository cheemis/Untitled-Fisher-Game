using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTreeToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        Vector3 lookAtDir = new Vector3(cameraPos.x,
                                        transform.rotation.eulerAngles.y,
                                        cameraPos.z);

        transform.LookAt(lookAtDir, Vector3.up);
    }
}
