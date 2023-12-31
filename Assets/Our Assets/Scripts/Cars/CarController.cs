using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CarController : MonoBehaviour
{
    public float dropForce;
    public enum PathType { Inner, Outer};
    public PathType pathType;
    public NavMeshAgent agent;
    public bool canDropTrashes = true;
    public Transform trashSpawner;
    public List<Transform>targets = new List<Transform>();
    Transform currentTarget;
    int currentTargetIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (pathType == PathType.Inner)
        {
            foreach (var node in PathManager.Instance.InnerpathNodes)
            {
                targets.Add(node.transform);
            }
        }
        else if (pathType == PathType.Outer)
        {
            foreach (var node in PathManager.Instance.OuterpathNodes)
            {
                targets.Add(node.transform);
            }
        }

        currentTargetIndex = 0;
        currentTarget = targets[currentTargetIndex];
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveCar();
        //agent.SetDestination(currentTarget.position);

        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    DropTrash();
        //}
    }

    public void MoveCar()
    {
        //Debug.Log(currentTargetIndex);
        agent.SetDestination(currentTarget.position);
        float distance = Vector3.Distance(agent.transform.position, currentTarget.position);
        if (distance > 1) return;
        DropTrash();
        currentTargetIndex++;
        if (currentTargetIndex == targets.Count)
        {
            CarManager.Instance.OnCarDestory(this);
        }
        else
        {
            currentTarget = targets[currentTargetIndex];
        }


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

        TrashCollectable droppedTrash = Instantiate(trashPrefab, trashSpawner.position, Quaternion.identity).GetComponent<TrashCollectable>();
        AudioManager.Instance.Play("DropTrash", 0.8f);
        droppedTrash.ThrowTrash(dropForce);
        //Debug.Log(droppedTrash.trashType);
        //if (droppedTrash.trashType == TrashCollectable.TrashType.Large)
        //{
        //    canDropTrashes = false;
        //}

    }

}
