using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    public EnemyProjectile container;

    void OnTriggerEnter(Collider other){
        container.hurtPlayer();
    }

}
