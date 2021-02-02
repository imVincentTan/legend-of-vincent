using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_flying_enemy_ai : MonoBehaviour
{
    public player_controller playerhealth;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayerMask, playerLayerMask;

    // patrolling
    public Vector3 destinationPoint;
    private bool destinationPointSet;
    private bool kitingPointSet;
    public float destinationPointRange;

    // attacking
    public float attackCooldown;
    public float attackCooldownFinish;
    private bool canAttack = true;

    public GameObject bulletPrefab;
    public float projectileSpeed;

    // state
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // stats
    public float attackPower = 3f;
    public float minHeight = 3f;
    public float maxHeight = 5f; 

    // misc
    private int failcount = 0;

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
        if(destinationPointSet){
            if(failcount > 3){
                print("boop bad object terminated");
                Destroy(gameObject);
            }
            if(!agent.SetDestination(destinationPoint)){
                failcount += 1;
                GetDestinationPoint();
            }else{
                failcount = 0;
            }
        }
        

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
            // should almost never go here
            GetDestinationPoint();
        }
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        
        if(canAttack){
            
            // attack

            // agent.SetDestination(transform.position);
            // transform.LookAt(player);
            print("chicken attack!!!");
            
            GameObject chickenBullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
            
            EnemyProjectile projectile = chickenBullet.GetComponent<EnemyProjectile>();
            projectile.setStraightProjectile(projectileSpeed, gameObject.transform, player, attackPower);

            // playerhealth.takeDamage(attackPower);
            canAttack = false;
            attackCooldownFinish = Time.time + attackCooldown;
            kitingPointSet = false;
        }else if(Time.time > attackCooldownFinish){
            canAttack = true;
        }else if(!kitingPointSet){
            float randomz = Random.Range(-attackRange,attackRange);
            float randomx = Random.Range(-attackRange,attackRange);

            destinationPoint = new Vector3(player.position.x + randomx, transform.position.y, player.position.z + randomz);
            agent.SetDestination(destinationPoint);
            kitingPointSet = true;
        }else{
            Vector3 distanceToDestinationPoint = destinationPoint - transform.position;

            if (distanceToDestinationPoint.magnitude < 1f) kitingPointSet = false;
        }
    }
}

