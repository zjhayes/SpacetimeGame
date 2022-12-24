using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EGController : MonoBehaviour
{
    [SerializeField]
    private Transform model;
    [SerializeField]
    private float awareDistance = 10.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private Transform[] navPoints;
    [SerializeField]
    private bool isPassive;
    [SerializeField]
    private float searchTime = 5.0f;
    [SerializeField]
    private float attackRange = 5.0f;

    State currentState = State.PATROLLING;
    private NavMeshAgent agent;
    private Transform goal;
    private Transform player;
    private Vector3 playerLastPosition;
    private int destinationPoint = 0;
    private float playerDistance;
    private bool pathBlocked = false;
    private float currentSearchTime = 0.0f;

    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;
    readonly float NUMBER_OF_RAYS = 24.0f;
    readonly float SIGHT_OFFSET_BOTTOM = -0.5f;
    readonly float SIGHT_OFFSET_TOP = 1.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = PlayerManager.instance.Player.transform;
        currentSearchTime = searchTime;

        agent.autoBraking = false;

        if(isPassive)
        {
            currentState = State.PASSIVE;
        }
    }

    void Update()
    {
        Scan();

        switch(currentState)
        {
            case(State.PASSIVE):
                return;
            case(State.PATROLLING):
                Patrol();
                return;
            case(State.ALERT):
                Pursue();
                return;
            case(State.SEARCHING):
                Search();
                return;
        }
    }

    void Scan()
    {
        // Check top and bottom sight lines for player.
        bool canSeeBottom = CanSeePlayer(SIGHT_OFFSET_BOTTOM, -60, 5);
        bool canSeeTop = CanSeePlayer(SIGHT_OFFSET_TOP, -40, 3.5f);
        bool playerInView = canSeeBottom || canSeeTop;

        if(playerInView)
        {
            currentState = State.ALERT;
        }
        else if(currentState == State.ALERT)
        {
            currentState = State.SEARCHING;
        }
    }

    // TODO: Remove DebugRays to make more efficient, update OR condition in Scan method.
    bool CanSeePlayer(float sightOffset, float startAngleOffset, float stepAngleOffset)
    {
        Quaternion startingAngle = Quaternion.AngleAxis(startAngleOffset, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(stepAngleOffset, Vector3.up);
        Quaternion angle = transform.rotation * startingAngle;
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += sightOffset;
        Vector3 forward = angle * Vector3.forward;
        bool playerInView = false;
        for(int i = 0; i < NUMBER_OF_RAYS; i++)
        {
            if(Physics.Raycast(position, forward, out hit, awareDistance))
            {
                if(hit.collider.tag == "Player")
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    playerLastPosition = player.transform.position;
                    playerInView = true;
                }
                else
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(position, forward * awareDistance, Color.white);
            }
            forward = stepAngle * forward;
        }
        return playerInView;
    }

    void Patrol()
    {
        Move();
        if(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE)
        {
            GotoNextPoint();
        }
    }

    void Pursue()
    {
        TargetPlayer();

        // Stop and attack when in range.
        if(agent.remainingDistance > attackRange)
        {
            Move();
        }
        else
        {
            Stop();
            LookAtPlayer();
            Attack();
        }
    }

    void Search()
    {
        if(currentSearchTime > 0f)
        {
            // Count down search timer.
            currentSearchTime -= Time.deltaTime;
        }
        else
        {
            // Patrol and reset clock.
            currentSearchTime = searchTime;
            currentState = State.PATROLLING;
        }
    }

    void Attack()
    {
        Debug.Log("EG Attacks " + Time.deltaTime);
    }

    void LookAtPlayer()
    {
        transform.LookAt(playerLastPosition);
    }

    void GotoNextPoint()
    {
        if(navPoints.Length == 0)
        {
            Debug.Log("No patrol route set. " + gameObject.name);
            return;
        }
        
        // Set destination to next patrol route point.
        agent.destination = navPoints[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % navPoints.Length;
    }
    void TargetPlayer()
    {
        agent.destination = playerLastPosition;
    }

    void Move()
    {
        agent.isStopped = false;
    }

    void Stop()
    {
        agent.isStopped = true;
    }
}

public enum State
{
    PASSIVE,
    PATROLLING,
    ALERT,
    SEARCHING,
    DEAD
}