using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private float lifeTime = 10f;

    public float lifeMax = 60f;
    public float lifeMin = 15f;

    public Sprite[] sprites;
    public int currentStage = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // randomize the lifetime
        lifeTime = Random.Range(lifeMin, lifeMax);
        // lifeTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // check if the tree is at the last stage
        if (currentStage == sprites.Length - 1)
        {
            return;
        }

        //  decrease the life time
        lifeTime -= Time.deltaTime;

        // if the life time is less than 0
        if (lifeTime < 0)
        {
            // check if object is off screen
            Vector3 vpPos = Camera.main.WorldToViewportPoint(transform.position);

            // check for edge
            // bool isVisible = vpPos.x >= 0f && vpPos.x <= 1f && vpPos.y >= 0f && vpPos.y <= 1f && vpPos.z > 0f;

            // check for edge also considering the size of the object
            float size = 60f;
            bool isVisible = vpPos.x >= -size && vpPos.x <= 1f + size && vpPos.y >= -size && vpPos.y <= 1f + size && vpPos.z > 0f;


            if (!isVisible)
            {
                // change to the next sprite
                currentStage++;

                // change the sprite
                GetComponent<SpriteRenderer>().sprite = sprites[currentStage];

                // reset the life time
                lifeTime = Random.Range(lifeMin, lifeMax);
            }
        }
    }
}
