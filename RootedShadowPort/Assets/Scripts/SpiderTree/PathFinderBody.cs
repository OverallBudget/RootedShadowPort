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
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            return;

        Vector3 target = navAgent.steeringTarget;
        direction = (target - transform.position).normalized;
        velocity = direction * navAgent.speed;

        navAgent.Move(velocity * Time.deltaTime);
    }
}