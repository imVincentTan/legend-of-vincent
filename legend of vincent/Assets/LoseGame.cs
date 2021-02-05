using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public bool inDialogue = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inDialogue && Input.GetButton("Submit")){
            inDialogue = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }

}
