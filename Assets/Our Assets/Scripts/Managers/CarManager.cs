using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public int  maxCar = 3;
    public GameObject carPrefab;
    public Transform carSpawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnNewCars();
        }
    }

    public void SpawnNewCars()
    {
        Instantiate(carPrefab,carSpawner.position,Quaternion.identity);
    }


}
