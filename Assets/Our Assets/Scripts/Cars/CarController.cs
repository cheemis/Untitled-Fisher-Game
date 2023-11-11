using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform>points = new List<Transform>();
    Transform currentTarget;
    int currentTargetIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentTargetIndex = 0;
        currentTarget = points[currentTargetIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(currentTargetIndex);
        agent.SetDestination(currentTarget.position);
        float distance = Vector3.Distance(agent.transform.position, currentTarget.position);
        if (distance > 1) return;
        currentTargetIndex++;
        if (currentTargetIndex == 4)
        {
            currentTargetIndex = 0;
        }
        currentTarget = points[currentTargetIndex];
        //agent.SetDestination(currentTarget.position);


    }
}
