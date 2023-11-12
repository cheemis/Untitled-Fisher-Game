using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : UnitySingleton<CarManager>
{
    public int  maxCar = 3;
    public GameObject carPrefab;
    public List<Transform> carSpawners = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewCars();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    SpawnNewCars();
        //}
    }

    public void SpawnNewCars()
    {
       for (int i = 0;i< carSpawners.Count;i++)
        {
            if (i == 0)
            {
                carPrefab.GetComponent<CarController>().pathType = CarController.PathType.Inner;
            }
            else if (i == 1)
            {
                carPrefab.GetComponent<CarController>().pathType = CarController.PathType.Outer;
            }
            Instantiate(carPrefab, carSpawners[i].position, Quaternion.identity);
        }

    }

    public void OnCarDestory(CarController car)
    {
        if (car.pathType== CarController.PathType.Inner)
        {
            Instantiate(carPrefab, carSpawners[0].position, Quaternion.identity);
        }
        else if (car.pathType == CarController.PathType.Outer)
        {
            Instantiate(carPrefab, carSpawners[1].position, Quaternion.identity);
        }
        Destroy(car.gameObject);
    }

}
