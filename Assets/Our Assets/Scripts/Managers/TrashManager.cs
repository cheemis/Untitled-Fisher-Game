using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : UnitySingleton<TrashManager>
{
    public List<GameObject> trashesInResorce = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ReturnRandomTrash()
    {
        int trashIndex = Random.Range(0, trashesInResorce.Count- 1) % (trashesInResorce.Count - 1);
        return trashesInResorce[trashIndex];
    }
}
