using UnityEngine;
using UnityEngine.AI;

public class SpiderTree : PathFinderBody
{
    private enum State { Patrol, Alerted }
    [SerializeField] private State currentState = State.Patrol;

    [SerializeField] private Transform patrolPath;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;

    [SerializeField] private Transform player;
    private Vector3 alertTarget;
    private bool hasAlertTarget = false;
    private Transform parent;

    private float patrolResumeTimer = 0f;
    private float alertCheckRadius = 30f;
    [SerializeField] private GameObject gameOverUI;
    protected override void Awake()
    {
        base.Awake();
        parent = transform.parent;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        patrolPoints = new Vector3[patrolPath.childCount];
        for (int i = 0; i < patrolPath.childCount; i++)
        {
            patrolPoints[i] = parent.TransformPoint(patrolPath.GetChild(i).localPosition);
        }
        SetTargetLocation(patrolPoints[patrolIndex]);
    }

    private void Update()
    {
        //Debug.Log($"[SpiderTree] State: {currentState}, Position: {transform.position}, Target: {navAgent.destination}");

        switch (currentState)
        {
            case State.Patrol:
                PatrolBehavior();
                break;

            case State.Alerted:
                AlertedBehavior();
                break;
        }
    }

    private void PatrolBehavior()
    {
        if (patrolResumeTimer > 0)
        {
            patrolResumeTimer -= Time.deltaTime;
            return;
        }

        if (hasAlertTarget)
        {
            currentState = State.Alerted;
            SetTargetLocation(alertTarget);
            hasAlertTarget = false;
            return;
        }

        if (!navAgent.pathPending && HasReachedDestination())
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            SetTargetLocation(patrolPoints[patrolIndex]);
        }
    }

    private void AlertedBehavior()
    {
        if (!navAgent.pathPending && HasReachedDestination())
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < alertCheckRadius)
            {
                SetTargetLocation(player.position);
            }
            else
            {
                ReturnToPatrol();
            }
        }
    }

    private void ReturnToPatrol()
    {
        currentState = State.Patrol;
        patrolIndex = 0;
        SetTargetLocation(patrolPoints[patrolIndex]);
        patrolResumeTimer = 0.5f;
    }

    private bool HasReachedDestination(float threshold = 0.3f)
    {
        Vector3 pos = transform.position;
        Vector3 dest = navAgent.destination;
        pos.y = dest.y = 0;
        return Vector3.Distance(pos, dest) <= threshold;
    }

    public void AlertTo(Vector3 position)
    {

            Debug.Log($"[SpiderTree] Alerted to: {position}");
            alertTarget = position;
            hasAlertTarget = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[SpiderTree] Player collided! Game Over!");
            Gameover();
        }
    }
    private void Gameover()
    {
        gameOverUI.SetActive(true);
    }
}
