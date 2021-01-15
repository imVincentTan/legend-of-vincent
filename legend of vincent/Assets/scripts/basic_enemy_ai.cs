using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class basic_enemy_ai : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayerMask, playerLayerMask;

    // patrolling
    public Vector3 destinationPoint;
    bool destinationPointSet;
    public float destinationPointRange;

    // attacking
    public float attackCooldown;
    bool attacked;

    // state
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,playerLayerMask);

        if(!playerInSightRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInAttackRange) AttackPlayer();
    }

    private void Patrolling(){
        if(!destinationPointSet) GetDestinationPoint();
        if(destinationPointSet) agent.SetDestination(destinationPoint);

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;

        if (distanceToDestinationPoint.magnitude < 1f) destinationPointSet = false;

    }

    private void GetDestinationPoint(){
        float randomz = Random.Range(-destinationPointRange,destinationPointRange);
        float randomx = Random.Range(-destinationPointRange,destinationPointRange);

        destinationPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, groundLayerMask)) destinationPointSet = true;
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        agent.SetDestination(transform.position);
        print("attack!!!");
        transform.LookAt(player);

        if(!attacked){
            // attack
            attacked = true;
        }
    }
}
