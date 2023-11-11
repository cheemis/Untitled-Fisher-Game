using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    // Start is called before the first frame update
    private float lifeTime = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  decrease the life time
        lifeTime -= Time.deltaTime;

        // if the life time is less than 0
        if (lifeTime < 0)
        {
            // check if object is off screen
            Vector3 vpPos = Camera.main.WorldToViewportPoint(transform.position);
            bool isVisible = vpPos.x >= 0f && vpPos.x <= 1f && vpPos.y >= 0f && vpPos.y <= 1f && vpPos.z > 0f;
            if (!isVisible)
            {
                // destroy the object
                Destroy(gameObject);
            }
        }
    }
}
