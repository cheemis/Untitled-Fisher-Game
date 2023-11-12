using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{

    //fish spawning variables
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private int maxFish = 10;
    private int currentFish = 0;

    //lake variables
    [SerializeField]
    private Vector2 hor = new Vector2 (40, -40);
    [SerializeField]
    private Vector2 vert = new Vector2 (40, -40);



    // Start is called before the first frame update
    void Start()
    {
        spawnFish();
    }



    // ===================================== //
    // ===== LISTENING/EVENT FUNCTIONS ===== //
    // ===================================== //

    private void OnEnable()
    {
        FishingGameManager.collectFish += FishCollected;
    }

    private void OnDisable()
    {
        FishingGameManager.collectFish -= FishCollected;
    }

    public void FishCollected()
    {
        currentFish--;

        if(Random.Range(0,100) < 50) //50% chance that the fish population will change
        {
            maxFish = Random.Range(maxFish - 1, maxFish + 1); //fish population may increase or decrease
        }

        spawnFish();
    }



    // ===================================== //
    // ====== SPAWNING FISH FUNCTIONS ====== //
    // ===================================== //

    private void spawnFish()
    {
        for(; currentFish < maxFish; currentFish++)
        {
            InstatiateFish();
        }
    }

    private void InstatiateFish()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(hor.x, hor.y),
                                            0,
                                            Random.Range(vert.x, vert.y));

        GameObject fish = Instantiate(fishPrefab, spawnPosition, fishPrefab.transform.rotation);
    }

}
