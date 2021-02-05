using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // self destroy
    public float startTime;
    public float explosionDuration = 1f;

    // audio
    public AudioSource audioSource;
    public AudioClip boomSound;
    public AudioClip duangSound;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        int temp = Random.Range(0,2);
        if(temp == 0){
            audioSource.PlayOneShot(boomSound);
        }else if(temp == 1){
            audioSource.PlayOneShot(duangSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + explosionDuration){
            Destroy(gameObject);
        }
    }
}
