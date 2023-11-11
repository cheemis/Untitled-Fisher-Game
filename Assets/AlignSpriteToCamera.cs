using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignSpriteToCamera : MonoBehaviour
{

    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(offsetX + Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, offsetZ);
    }
}
