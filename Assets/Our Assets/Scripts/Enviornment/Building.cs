using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private float timeToAppear = .5f;
    private bool canRender = false;

    private SpriteRenderer buildingSprite;

    private void Start()
    {
        buildingSprite = GetComponentInChildren<SpriteRenderer>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        if(canRender)
        {
            UnityEngine.Color c = buildingSprite.color;
            c.a = Mathf.Clamp(c.a + ((1/ timeToAppear) * Time.deltaTime), 0, 1);

            Debug.Log("c.a: " + c.a);

            if (c.a >= .95)
            {
                canRender = false;
                c.a = 1;
            }
            buildingSprite.color = c;
        }

        if(Input.GetKeyDown(KeyCode.L)) { ShowBuilding(); }
    }

    public void ShowBuilding()
    {
        Invoke("StartShowing", Random.Range(5, 10));
    }

    private void StartShowing()
    {
        canRender = true;

        UnityEngine.Color c = buildingSprite.color;
        c.a = 0;
        buildingSprite.color = c;


        transform.GetChild(0).gameObject.SetActive(true);
    }
}
