using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : UnitySingleton<TrashManager>
{
    public List<GameObject> trashesInResource = new List<GameObject>();
    public Transform pond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ReturnRandomTrash()
    {
        int weightedTotal = 0;
        foreach (var t in trashesInResource)
        {
            weightedTotal += t.GetComponent<TrashCollectable>().weight;
        }
        //Debug.Log("Total Weight: " + weightedTotal);
        int randomWeight = Random.Range(0, weightedTotal) % weightedTotal;

        for (int i = 0; i< trashesInResource.Count; i++)
        {
           TrashCollectable trash = trashesInResource[i].GetComponent<TrashCollectable>();
            randomWeight -= trash.weight;
            if (randomWeight < 0)
            {
                //Debug.Log("Trash Index: " + i);
                return trashesInResource[i];
            }
        }
        return null;
    }

}
