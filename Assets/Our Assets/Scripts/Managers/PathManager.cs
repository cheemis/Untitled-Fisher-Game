using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : UnitySingleton<PathManager>
{
    public List<GameObject> pathNodes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < pathNodes.Count; i++)
        {
            PathNode node = pathNodes[i].GetComponent<PathNode>();
            // set the first node's previous Node as the last node in the list
            if (i == 0)
            {
             
                node.previousNode = pathNodes[pathNodes.Count - 1].GetComponent<PathNode>();
            }
            else
            {
               
               node.previousNode = pathNodes[i - 1].GetComponent<PathNode>();
            }

            // set the last node's next Node as the first node in the list
            if (i == pathNodes.Count - 1)
            {
               
                node.nextNode = pathNodes[0].GetComponent<PathNode>();
            }
            else
            {
                
                node.nextNode = pathNodes[i + 1].GetComponent<PathNode>();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
