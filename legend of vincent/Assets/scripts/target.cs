using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target : MonoBehaviour
{
    public GameObject enemyContainer;

    public GameObject explosionPrefab;

    public Canvas canvas;
    public Slider slider;

    // stats
    public float maxHealth = 30;
    public float health = 30;


    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void takeDamage(float damageTaken){
        health -= damageTaken;
        if(health <= 0) die();
        canvas.enabled = true;
        slider.value = health / maxHealth;
    }

    private void die(){
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(enemyContainer);
    }
}
