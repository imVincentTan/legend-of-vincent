using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float timeCreated;
    public float expiryTime;

    private float projectileSpeed;
    private Transform startPosition;
    private Transform endPosition;
    private Vector3 direction;

    private int flightPath = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeCreated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeCreated + expiryTime){
            Destroy(gameObject);
        }

        if(flightPath == 1){
            print("gogogo");
            // gameObject.transform.Translate(direction * Time.deltaTime * projectileSpeed);
            // gameObject.transform.Translate(-gameObject.transform.forward * Time.deltaTime * projectileSpeed);
            gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * projectileSpeed;
            
            if ((gameObject.transform.position - endPosition.position).magnitude < 0.5f){
                print("byebye world");
                Destroy(gameObject);
            }
            
        }
    }

    public void setStraightProjectile(float pSpeed, Transform sPos, Transform ePos){
        projectileSpeed = pSpeed;
        startPosition = sPos;
        endPosition = ePos;
        flightPath = 1;
        
        gameObject.transform.position = startPosition.position;
        gameObject.transform.LookAt(endPosition);

        // direction = endPosition.position - startPosition.position;
        // direction.Normalize();

        // Quaternion rotation = Quaternion.LookRotation(direction);
        // transform.rotation = rotation;


    }
}
