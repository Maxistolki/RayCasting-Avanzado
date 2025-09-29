using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentScript : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] bool isPatrolling = true;
    [SerializeField] float arrivalDistance = 0.5f;
    [SerializeField] Animator anim;
    [SerializeField] float velocity;
    [SerializeField] Transform currentDestinantion;
    [SerializeField] int currentPatrolPointIndex;
    [SerializeField] RaycastSight raycast;
    [SerializeField] Transform player;
    [SerializeField] float cooldown = 2;

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
        agent.destination = currentDestinantion.position;

        if (raycast.raycastInfo == "Player")
        {
            cooldown = 2;
            agent.destination = player.position;
            Debug.Log(agent.destination);

        }
        if ((raycast.raycastInfo != "Player" || !  raycast.estaTocando) && cooldown >= 0)
        {
            agent.destination = player.position;
            cooldown -= Time.deltaTime;
        }
        else if (agent.hasPath && agent.remainingDistance <= arrivalDistance && cooldown <= 0) // hasPath --> si ya calculó el recorrido
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

        

        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed",velocity);
  
    }
}
