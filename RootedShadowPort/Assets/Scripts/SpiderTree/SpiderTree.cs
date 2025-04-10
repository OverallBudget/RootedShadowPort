using UnityEngine;
using UnityEngine.AI;

public class SpiderTree : PathFinderBody
{
    private enum States { PATROL, CHASE }
    [SerializeField] private States state = States.PATROL;

    [SerializeField] private Transform patrolPath;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;

    private Transform player;
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
            patrolPoints[i] = patrolPath.GetChild(i).position;
        }

        patrolIndex = 0;
        SetTargetLocation(parent.TransformPoint(patrolPoints[patrolIndex]));
    }
}
