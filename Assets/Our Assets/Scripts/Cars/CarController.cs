using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool canDropTrashes = false;
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
    void FixedUpdate()
    {

     
        
        //agent.SetDestination(currentTarget.position);


    }

    public void MoveCar()
    {
        //Debug.Log(currentTargetIndex);
        //agent.SetDestination(currentTarget.position);
        //float distance = Vector3.Distance(agent.transform.position, currentTarget.position);
        //if (distance > 1) return;
        //currentTargetIndex++;
        //if (currentTargetIndex == targets.Count)
        //{
        //    currentTargetIndex = 0;
        //}
        //currentTarget = targets[currentTargetIndex];
    }

    public void OnSpawn()
    {
        MoveCar();
    }

    public void DropTrash()
    {
        GameObject trashPrefab = TrashManager.Instance.ReturnRandomTrash();
        Instantiate(trashPrefab, trashSpawner);

        TrashController droppedTrash = trashPrefab.GetComponent<TrashController>();
        if (droppedTrash.trashType == TrashController.TrashType.Large)
        {
            canDropTrashes = false;
        }

        //Instantiate();
    }

}
