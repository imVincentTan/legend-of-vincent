using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target : MonoBehaviour
{
    public GameObject enemyContainer;

    public GameObject explosionPrefab;

    public GameObject winGameDialogue;

    public Canvas canvas;
    public Slider slider;

    public bool isFinalBoss = false;

    // stats
    public float maxHealth = 30;
    public float health = 30;

    public PauseMenuController pauseMenuController;

    // audio
    public AudioSource audioSource;
    public AudioClip yahooSound;
    public AudioClip yaySound;


    // Start is called before the first frame update
    void Start()
    {   
        if(!isFinalBoss) canvas.enabled = false;
    }


    
    public void takeDamage(float damageTaken){
        health -= damageTaken;
        if(health <= 0) die();
        if(!isFinalBoss) canvas.enabled = true;
        slider.value = health / maxHealth;
    }

    private void die(){
        if(isFinalBoss){
            pauseMenuController.gameOver = true;
            Cursor.lockState = CursorLockMode.None;
            winGameDialogue.SetActive(true);
            Time.timeScale = 0f;

            int temp = Random.Range(0,2);
            if(temp == 0){
                audioSource.PlayOneShot(yahooSound);
            }else if(temp == 1){
                audioSource.PlayOneShot(yaySound);
            }
        }
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(enemyContainer);
    }
}

