using UnityEngine;
using UnityEngine.AI;

public class SpiderTree : PathFinderBody
{
    private enum States { PATROL, CHASE }
    [SerializeField] private States state = States.PATROL;

    [SerializeField] private Transform patrolPath;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;

    [SerializeField] Transform player;
    Vector3 lastPlayerPos;
    float chaseUpdateThreshold = 1.0f;
    private Transform parent;

    protected override void Awake()
    {
        base.Awake();
        parent = transform.parent;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;


        patrolPoints = new Vector3[patrolPath.childCount];
        for (int i = 0; i < patrolPath.childCount; i++)
        {
            patrolPoints[i] = parent.TransformPoint(patrolPath.GetChild(i).localPosition);
        }
        SetTargetLocation(patrolPoints[patrolIndex]);
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());

    }
    private float patrolResumeTimer = 0f;
    private void Update()
    {
        Debug.Log($"[SpiderTree] State: {state}, Position: {transform.position}, Target: {navAgent.destination}");
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log($"Distance to Player: {distanceToPlayer}");
        switch (state)
        {
            case States.PATROL:
                if (distanceToPlayer < 50f)
                {
                    state = States.CHASE;
                    SetTargetLocation(player.position);
                }
                else {
                    if (patrolResumeTimer > 0)
                    {
                        patrolResumeTimer -= Time.deltaTime;
                    }
                    else
                    {
                        PathFollow();
                    }
                }
                break;

            case States.CHASE:
                 float distFromLast = Vector3.Distance(player.position, lastPlayerPos);
                if (distanceToPlayer < 30f)
                {
                    if (distFromLast > chaseUpdateThreshold)
                    {
                        lastPlayerPos = player.position;
                        SetTargetLocation(player.position);
                    }
                }
                else
                {
                    state = States.PATROL;
                     navAgent.ResetPath();
                    patrolIndex = 0;
                    SetTargetLocation(patrolPoints[patrolIndex]);
                    patrolResumeTimer = 0.5f;
                }
                break;
        }
    }

    private void PathFollow()
    { 
        if (!navAgent.pathPending && HasReachedDestination())
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            SetTargetLocation(patrolPoints[patrolIndex]);
        }

    }

    private bool HasReachedDestination(float threshold = 0.3f)
    {
        Vector3 pos = transform.position;
        Vector3 dest = navAgent.destination;

        pos.y = dest.y = 0;

        return Vector3.Distance(pos, dest) <= threshold;
    }
}

