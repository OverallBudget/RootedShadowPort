using UnityEngine;
using UnityEngine.AI;

public class PathFinderBody : MonoBehaviour
{
    protected Vector3 direction = Vector3.zero;
    protected Vector3 velocity = Vector3.zero;

    protected NavMeshAgent navAgent;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    protected void SetTargetLocation(Vector3 position)
    {
        navAgent.SetDestination(position);
    }

    protected void Pathfinding()
    {
            }
}