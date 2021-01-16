using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public GameObject enemyContainer;


    // stats
    public float health = 30;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void takeDamage(float damageTaken){
        health -= damageTaken;
        if(health <= 0) die();
    }

    private void die(){
        Destroy(enemyContainer);
    }
}
