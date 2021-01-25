using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class basic_enemy_ai : MonoBehaviour
{

    public player_controller playerhealth;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayerMask, playerLayerMask;

    // patrolling
    public Vector3 destinationPoint;
    bool destinationPointSet;
    public float destinationPointRange;

    // attacking
    public float attackCooldown;
    public float attackCooldownFinish;
    bool canAttack = true;

    // state
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // stats
    public float attackPower = 5f;

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
        

        if (playerInAttackRange){
            AttackPlayer();
        }else if(playerInSightRange){
            ChasePlayer();
        }else{
            Patrolling();
        }
    }

    private void Patrolling(){
        
        if(!destinationPointSet) GetDestinationPoint();
        if(destinationPointSet) agent.SetDestination(destinationPoint);

        Vector3 distanceToDestinationPoint = destinationPoint - transform.position;

        if (distanceToDestinationPoint.magnitude < 1f) destinationPointSet = false;

    }

    private void GetDestinationPoint(){
        float randomz = Random.Range(-destinationPointRange,destinationPointRange);
        float randomx = Random.Range(-destinationPointRange,destinationPointRange);

        destinationPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, groundLayerMask)){
            
            destinationPointSet = true;
        }else{
            
            GetDestinationPoint();
        }
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        
        if(canAttack){
            
            // attack
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            print("attack!!!");
            playerhealth.takeDamage(attackPower);
            canAttack = false;
            attackCooldownFinish = Time.time + attackCooldown;
        }else if(Time.time > attackCooldownFinish){
            canAttack = true;
        }
    }

    

}
