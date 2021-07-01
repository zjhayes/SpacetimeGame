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
    private float awareDistance = 10f;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Transform[] navPoints;
    [SerializeField]
    private bool isPassive;

    private float scanDistance = 5.0f;
    private float scanRadius = 5.0f;


    State currentState = State.PATROLLING;
    private NavMeshAgent agent;
    private Transform goal;
    private Transform player;
    private Transform playerLastPosition;
    private int destinationPoint = 0;
    private float playerDistance;
    private bool pathBlocked = false;

    readonly float PATROL_POINT_MIN_DISTANCE = 0.5f;
    readonly float NUMBER_OF_RAYS = 24.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = PlayerManager.instance.Player.transform;

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
        }
    }

    void Scan()
    {
        Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
        Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
        Quaternion angle = transform.rotation * startingAngle;
        RaycastHit hit;
        Vector3 position = transform.position;
        Vector3 forward = angle * Vector3.forward;

        for(int i = 0; i < NUMBER_OF_RAYS; i++)
        {
            if(Physics.Raycast(position, forward, out hit, scanDistance))
            {
                if(hit.collider.tag == "Player")
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.red);
                    playerLastPosition = player.transform;
                    currentState = State.ALERT;
                }
                else
                {
                    Debug.DrawRay(position, forward * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(position, forward * scanRadius, Color.white);
            }
            forward = stepAngle * forward;
        }
    }

    void Patrol()
    {
        if(agent.remainingDistance < PATROL_POINT_MIN_DISTANCE)
        {
            GotoNextPoint();
        }
    }

    void Pursue()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        LookAtPlayer();
        Chase();

        if(playerDistance > awareDistance)
        {
            currentState = State.PATROLLING;
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(playerLastPosition);
    }

    void GotoNextPoint()
    {
        if(navPoints.Length == 0)
        {
            return;
        }
        
        agent.destination = navPoints[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % navPoints.Length;
    }

    void Chase()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

public enum State
{
    PASSIVE,
    PATROLLING,
    ALERT,
    DEAD
}
