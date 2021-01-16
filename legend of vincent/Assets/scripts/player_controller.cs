using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public float jumpHeight = 2f;
    public float jumpCheckDelay = 0.2f;

    Vector3 velocity;
    bool isGrounded = true;
    float lastTimeJumped = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player dies from Y position (out of world)

        // GroundCheck
        GroundCheck();

        // fall down

        // crouching

        // update height (Y value) of character

        // character movement
        HandleCharacterMovement();
        
        
        
    }

    // check if the player is close to the ground and set velocity
    // doesn't work if the player's feet are in the ground
    void GroundCheck(){
        // only check if there has been enough time since last jump
        if (Time.time >= lastTimeJumped + jumpCheckDelay){
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);

            if (isGrounded && velocity.y < 0){
                
                velocity.y = -2f;
            }
        }
    }

    void HandleCharacterMovement(){
        // WASD movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Jump movement
        if (Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        lastTimeJumped = Time.time;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}