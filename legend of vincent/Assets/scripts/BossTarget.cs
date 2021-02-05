using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTarget : MonoBehaviour
{
    public GameObject enemyContainer;

    public GameObject explosionPrefab;

    public Slider slider;

    // stats
    public float maxHealth = 30;
    public float health = 30;
    
    public void takeDamage(float damageTaken){
        health -= damageTaken;
        if(health <= 0) die();
        slider.value = health / maxHealth;
    }

    private void die(){
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(enemyContainer);
    }
}
