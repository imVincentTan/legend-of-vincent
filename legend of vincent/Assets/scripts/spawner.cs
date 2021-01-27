using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // enemy prefabs
    public GameObject smallCupcakePrefab;
    public GameObject bigCupcakePrefab;
    public GameObject smallChickenPrefab;
    public GameObject bigChickenPrefab;

    // spawner area
    private float centerx = 500f;
    private float centery = 500f;
    private float radius = 400f;
    // 400^2 = 160000

    private RaycastHit hitInfo;    
    public LayerMask groundLayerMask;

    // generated points
    private float genAngle;
    private float genDistance;
    private int genEnemyType;
    private float spawnPointx;
    private float spawnPointy;
    private Vector3 tempOrigin;

    // spawnrate managing
    public float maxNumberOfEnemies = 100f;
    private float lastEnemySpawnTime = 0f;
    public float newEnemySpawnTime = 10f;
    private float numberOfEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        // initial enemies
        for (int a = 0; a < maxNumberOfEnemies; a++){
            do{
                genAngle = Random.Range(0f, 360f);
                genDistance = Random.Range(0f,radius);
                genEnemyType = Random.Range(0,4);

                spawnPointx = Mathf.Cos(genAngle) * genDistance;
                spawnPointy = Mathf.Sin(genAngle) * genDistance;
                tempOrigin = new Vector3(centerx + spawnPointx,200,centery + spawnPointy);
            } while (!Physics.Raycast(tempOrigin, -transform.up, out hitInfo, 200f, groundLayerMask));
    
            if (genEnemyType == 0){
                Instantiate(smallCupcakePrefab, new Vector3(centerx + spawnPointx, 199.5f - hitInfo.distance, centery + spawnPointy), Quaternion.identity);
            }else if (genEnemyType == 1){
                Instantiate(bigCupcakePrefab, new Vector3(centerx + spawnPointx, 199.5f - hitInfo.distance, centery + spawnPointy), Quaternion.identity);
            }else if (genEnemyType == 2){
                Instantiate(smallChickenPrefab, new Vector3(centerx + spawnPointx, 199.5f - hitInfo.distance, centery + spawnPointy), Quaternion.identity);
            }else if (genEnemyType == 3){
                Instantiate(bigChickenPrefab, new Vector3(centerx + spawnPointx, 199.5f - hitInfo.distance, centery + spawnPointy), Quaternion.identity);
            }
            

        }


        // print("SPAWN START!");
        // float tempx = 734;
        // float tempy = 283;
        // Vector3 tempOrigin = new Vector3(tempx,200,tempy);

        // Physics.Raycast(tempOrigin, -transform.up, out hitInfo, 200f, groundLayerMask);

        // Instantiate(bigCupcakePrefab, new Vector3(tempx, 199.5f - hitInfo.distance, tempy), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
