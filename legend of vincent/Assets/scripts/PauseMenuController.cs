using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public bool gamePaused = false;
    public bool gameOver = false;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver){
            if (Input.GetButtonDown("Pause")){
                if (gamePaused){
                    // print("resume game!");
                    resumeGame();
                }else{
                    // print("pause game!");
                    pauseGame();
                }
            }
        }
    }

    public void resumeGame(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void pauseGame(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
