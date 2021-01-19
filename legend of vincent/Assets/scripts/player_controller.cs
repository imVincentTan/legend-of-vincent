﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player_controller : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 50f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public float jumpHeight = 2f;
    public float jumpCheckDelay = 0.2f;

    Vector3 velocity;
    bool isGrounded = true;
    float lastTimeJumped = 0f;

    float health = 100f;

    bool timeSlowed = false;
    float timeSlowDuration = 2f;
    float unTimeSlowTime = 0f;
    float timeSlowCooldown = 20f;
    float timeSlowCooldownOver = 0f;

    bool shieldDown = false;
    float shieldCooldown = 20f;
    float shieldCooldownOver = 0f;

    bool inDash = false;
    bool canDash = true;
    float dashTime = 0.1f;
    float dashOver = 0f;
    float dashCooldown = 5f;
    float dashCooldownOver = 0f;
    float dashSpeed = 200f;
    public GameObject camera;

    // HUD 
    public GameObject healthAmount;
    public Image time_slow_cover;
    public Image shield_cover;
    public Image dash_cover;
    public TMP_Text time_slow_counter;
    public TMP_Text shield_counter;
    public TMP_Text dash_counter;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player dies from Y position (out of world)

        // cooldowns
        if (timeSlowed && Time.time > unTimeSlowTime){
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f;
            timeSlowCooldownOver = Time.time + timeSlowCooldown;
            timeSlowed = false;
        }

        if(shieldDown && Time.time > shieldCooldownOver){
            shieldDown = false;
        }

        if(!inDash && Time.time > dashCooldownOver){
            canDash = true;
        }

        // cooldown UIs
        if (!timeSlowed && Time.time > timeSlowCooldownOver){
            time_slow_cover.fillAmount = 0f;
            time_slow_counter.gameObject.SetActive(false);
        }else if(timeSlowed){
            time_slow_cover.fillAmount = 1f;
            time_slow_counter.gameObject.SetActive(false);
        }else{
            time_slow_cover.fillAmount = (timeSlowCooldownOver-Time.time)/timeSlowCooldown;
            time_slow_counter.gameObject.SetActive(true);
            time_slow_counter.text = Mathf.RoundToInt(timeSlowCooldownOver-Time.time).ToString();
        }

        if(!shieldDown){
            shield_cover.fillAmount = 0f;
            shield_counter.gameObject.SetActive(false);
        }else{
            shield_cover.fillAmount = (shieldCooldownOver-Time.time)/shieldCooldown;
            shield_counter.gameObject.SetActive(true);
            shield_counter.text = Mathf.RoundToInt(shieldCooldownOver-Time.time).ToString();

        }

        if(canDash){
            dash_cover.fillAmount = 0f;
            dash_counter.gameObject.SetActive(false);
        }else if(inDash){
            dash_cover.fillAmount = 1f;
            dash_counter.gameObject.SetActive(false);
        }else{
            dash_cover.fillAmount = (dashCooldownOver-Time.time)/dashCooldown;
            dash_counter.gameObject.SetActive(true);
            dash_counter.text = Mathf.RoundToInt(dashCooldownOver-Time.time).ToString();
        }



        // GroundCheck
        GroundCheck();

        // character movement
        HandleCharacterMovement();

        // character abilities
        HandleCharacterAbility();
        


        
        
    }

    // check if the player is close to the ground and set velocity
    // doesn't work if the player's feet are in the ground
    private void GroundCheck(){
        // only check if there has been enough time since last jump
        if (Time.time >= lastTimeJumped + jumpCheckDelay){
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);

            if (isGrounded && velocity.y < 0){
                
                velocity.y = -2f;
            }
        }
    }

    private void HandleCharacterMovement(){
        if(inDash){
            controller.Move(camera.transform.forward * dashSpeed * Time.deltaTime);
            if(Time.time > dashOver){
                inDash = false;
                dashCooldownOver = Time.time + dashCooldown;
            }

        }else{
            // WASD movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            // Jump movement
            if (Input.GetButtonDown("Jump")){
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            lastTimeJumped = Time.time;

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        
    }

    public void takeDamage(float damageTaken){
        if(shieldDown){
            health -= damageTaken;
            healthAmount.GetComponent<Text>().text = health.ToString();
            if(health <= 0) die();
        }else{
            shieldDown = true;
            shieldCooldownOver = Time.time + shieldCooldown;
        }
        
    }

    private void die(){
        print("you die");
    }

    private void HandleCharacterAbility(){
        if (Input.GetKeyDown(KeyCode.Q)){
            timeSlow();
        }

        if (Input.GetKeyDown(KeyCode.E)){
            if(canDash){
                inDash = true;
                canDash = false;
                dashOver = Time.time + dashTime;
            }
        }

    }

    private void timeSlow(){
        if (!timeSlowed && Time.time > timeSlowCooldownOver){
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.5f;
            unTimeSlowTime = Time.time + timeSlowDuration;
            timeSlowed = true;
        }
    }
}