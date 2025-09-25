using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] bool isPatrolling = true;
    [SerializeField] float arrivalDistance = 0.5f;
    [SerializeField] Animator anim;
    [SerializeField] float velocity;
    [SerializeField] Transform currentDestinantion;
    [SerializeField] int currentPatrolPointIndex;
    [SerializeField] RaycastSight raycast;
    [SerializeField] Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDestinantion = patrolPoints[0];
        currentPatrolPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (raycast.raycastInfo == "Player")
        {
            agent.destination = player.position;
        }
        else if (agent.hasPath && agent.remainingDistance <= arrivalDistance) // hasPath --> si ya calculó el recorrido
        {
            if (currentPatrolPointIndex < patrolPoints.Length - 1)
            {
                currentPatrolPointIndex++;
            }
            else
            {
                currentPatrolPointIndex = 0;
            }
            currentDestinantion = patrolPoints[currentPatrolPointIndex];
        }

        

        agent.destination = currentDestinantion.position;
        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed",velocity);
  
    }
}
