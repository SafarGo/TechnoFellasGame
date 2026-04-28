using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.AI;

public class NPCBehavuir : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();

    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (points.Count == 0)
        {
            Debug.LogWarning("NPCBehaviour: points list is empty!");
            return;
        }

        MoveToPoint(currentPointIndex);
    }

    private void Update()
    {
        if (points.Count == 0)
            return;

        if (agent.pathPending)
            return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPointIndex++;

            if (currentPointIndex >= points.Count)
                currentPointIndex = 0;

            MoveToPoint(currentPointIndex);
        }
    }

    private void MoveToPoint(int index)
    {
        agent.SetDestination(points[index].position);
    }


}
