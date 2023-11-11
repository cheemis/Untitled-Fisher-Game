using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollectables : MonoBehaviour
{
    // This is a test script for boat tweaking purposes


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            float x = Random.Range(-40, 40);
            float z = Random.Range(-50, 40);
            transform.position = new Vector3(x, .5f, z);
        }
    }
}
