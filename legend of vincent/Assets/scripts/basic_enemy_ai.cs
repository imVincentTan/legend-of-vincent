using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AI;

public class basic_enemy_ai : MonoBehaviour
{
    public player_controller playerhealth;
    public UnityEngine.AI.NavMeshAgent agent;
    private Transform player;
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
        playerhealth = GameObject.Find("Player").GetComponent<player_controller>();

        Vector3 temp = gameObject.transform.position;
        temp.y = 53;
        gameObject.transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,playerLayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,playerLayerMask);
        
        try{
            if (playerInAttackRange){
                AttackPlayer();
            }else if(playerInSightRange){
                ChasePlayer();
            }else{
                Patrolling();
            }    
        }catch{
            print("does it ever go here? find out next time on...");
            Destroy(gameObject);
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

        if (Physics.Raycast(destinationPoint, -transform.up, 200f, groundLayerMask)){
            
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
            playerhealth.takeDamage(attackPower);
            canAttack = false;
            attackCooldownFinish = Time.time + attackCooldown;
        }else if(Time.time > attackCooldownFinish){
            canAttack = true;
        }
    }

    

}
