using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private int index = -1;
    [SerializeField]
    private int[] randomIndexes;

    private void Start()
    {
        RandomizeSpawnOrder();
    }

    private void OnEnable()
    {
        FishingGameManager.collectFish += AddBuilding;
    }

    private void OnDisable()
    {
        FishingGameManager.collectFish -= AddBuilding;
    }

    public void AddBuilding()
    {
        float numChildren = transform.childCount;
        index++;

        if(index < numChildren)
        {
            transform.GetChild(randomIndexes[index]).gameObject.GetComponent<Building>().ShowBuilding();
        }
    }

    private void RandomizeSpawnOrder()
    {
        randomIndexes = new int[transform.childCount];

        for(int i = 0; i < randomIndexes.Length; i++)
        {
            randomIndexes[i] = i;
        }

        for (int i = 0; i < randomIndexes.Length; i++)
        {
            int randIndex = Random.Range(i, randomIndexes.Length);
            int temp = randomIndexes[i];
            randomIndexes[i] = randomIndexes[randIndex];
            randomIndexes[randIndex] = temp;
        }

    }

}
