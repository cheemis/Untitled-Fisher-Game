using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool canDropTrashes = true;
    public Transform trashSpawner;
    public List<Transform>targets = new List<Transform>();
    Transform currentTarget;
    int currentTargetIndex;
    // Start is called before the first frame update
    void Start()
    {
        foreach( var node in PathManager.Instance.pathNodes )
        {
            targets.Add( node.transform );
        }
        currentTargetIndex = 0;
        currentTarget = targets[currentTargetIndex];
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveCar();
        //agent.SetDestination(currentTarget.position);


    }

    public void MoveCar()
    {
        Debug.Log(currentTargetIndex);
        agent.SetDestination(currentTarget.position);
        float distance = Vector3.Distance(agent.transform.position, currentTarget.position);
        if (distance > 1) return;
        DropTrash();
        currentTargetIndex++;
        if (currentTargetIndex == targets.Count)
        {
            currentTargetIndex = 0;
        }
        currentTarget = targets[currentTargetIndex];

    }


    public void DropTrash()
    {
        if (!canDropTrashes) return;
        PathNode currentPathNode = currentTarget.GetComponent<PathNode>();
        float rngValue = Random.Range(0.0f, 1.0f) % 1.0f;
        //Debug.Log("rngValue: " + rngValue);
        //Debug.Log("dropChance: " + currentPathNode.dropTrashChance);
        if (rngValue > currentPathNode.dropTrashChance) return;
        GameObject trashPrefab = TrashManager.Instance.ReturnRandomTrash();
        Instantiate(trashPrefab, trashSpawner.position,Quaternion.identity);

        TrashCollectable droppedTrash = trashPrefab.GetComponent<TrashCollectable>();
        Debug.Log(droppedTrash.trashType);
        if (droppedTrash.trashType == TrashCollectable.TrashType.Large)
        {
            canDropTrashes = false;
        }

        //Instantiate();
    }

}
