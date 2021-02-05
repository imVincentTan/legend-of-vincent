using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalbossChickenAI : MonoBehaviour
{
    public player_controller playerhealth;
    public Transform player;
    public LayerMask groundLayerMask, playerLayerMask;

    // attacking
    public float attackCooldown;
    public float attackCooldownFinish;
    private bool canAttack = true;

    public GameObject bulletPrefab;
    public float projectileSpeed;

    // stats
    public float attackPower = 3f;

    // audio
    public AudioSource audioSource;
    public AudioClip chickenSound0;
    public AudioClip chickenSound1;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
        
        transform.LookAt(player);
    }

    
    private void AttackPlayer(){
        
        if(canAttack){
            print("big chicken attack!!!");

            int temp = Random.Range(0,2);
            if(temp == 0){
                audioSource.PlayOneShot(chickenSound0);
            }else if(temp == 1){
                audioSource.PlayOneShot(chickenSound1);
            }
            
            GameObject chickenBullet = Instantiate(bulletPrefab, new Vector3(0,0,0), Quaternion.identity);
            
            EnemyProjectile projectile = chickenBullet.GetComponent<EnemyProjectile>();
            projectile.setStraightProjectile(projectileSpeed, gameObject.transform, player, attackPower);

            // playerhealth.takeDamage(attackPower);
            canAttack = false;
            attackCooldownFinish = Time.time + attackCooldown;
        }else if(Time.time > attackCooldownFinish){
            canAttack = true;
        }
    }
}
