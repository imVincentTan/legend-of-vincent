using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAggregateController : MonoBehaviour
{
    public int totalCollected = 0;

    public bool inDialogue = false;

    public GameObject dialogueScreen;
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject thirdScreen;

    // final boss and UI elements
    public GameObject bossOverlay;
    public GameObject finalBoss;

    // audio
    public AudioSource audioSource;
    public AudioClip yahooSound;
    public AudioClip yaySound;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inDialogue && Input.GetButton("Submit")){
            Time.timeScale = 1f;
            dialogueScreen.SetActive(false);
        }
    }

    public void startDialogue(){
        int temp = Random.Range(0,2);
        if(temp == 0){
            audioSource.PlayOneShot(yahooSound);
        }else if(temp == 1){
            audioSource.PlayOneShot(yaySound);
        }
        Time.timeScale = 0f;
        inDialogue = true;
        if (totalCollected == 1){
            dialogueScreen.SetActive(true);
            firstScreen.SetActive(true);
            secondScreen.SetActive(false);
            thirdScreen.SetActive(false);
        }else if (totalCollected == 2){
            dialogueScreen.SetActive(true);
            firstScreen.SetActive(false);
            secondScreen.SetActive(true);
            thirdScreen.SetActive(false);
        }else if (totalCollected == 3){
            dialogueScreen.SetActive(true);
            firstScreen.SetActive(false);
            secondScreen.SetActive(false);
            thirdScreen.SetActive(true);

            bossOverlay.SetActive(true);
            finalBoss.SetActive(true);
        }

    }
}
