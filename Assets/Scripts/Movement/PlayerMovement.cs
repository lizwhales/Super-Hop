using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    
    public float airSpeed;
    public float speed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCD;
    public float airMultipler;
    public float wallRunSpeed;
    bool jumpReady;

    public Transform orientation;
    float leftRight;
    float upDown;

    [Header("onSurface")]

    public float playerHeight;
    public float coyoteTime;
    public LayerMask Ground;
 
    bool grounded;
    
    float groundedTime;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    // new stuff here
    public MovementState state;
    public enum MovementState{
        walljumping, 
        wallsliding
    }

    public bool walljumping;
    public bool wallsliding;
    Vector3 moveDirection;
    Rigidbody rb;

    private void Start(){
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpReady = true;
        groundedTime = coyoteTime;
    }

    private void Update(){
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (!grounded) {
            groundedTime -= Time.deltaTime;
            if (groundedTime > 0.00F) {
                grounded = true;
            }
        } else {
            groundedTime = coyoteTime;
        }

        // Drag
        var v = rb.velocity;
        v.y = 0f;
        v = -v * v.magnitude;
        rb.AddForce(groundDrag * v);

        PlayerInput();
        LimitSpeed();
        StateHandler();
        MovePlayer();


    }

    private void PlayerInput(){
        leftRight = Input.GetAxisRaw("Horizontal");
        upDown = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && grounded && jumpReady){
            jumpReady = false;
            grounded = false;
            groundedTime = 0.0F;
            Jump();

            // jump over and over again whn pressing space
            Invoke(nameof(ResetJump), jumpCD);
        }

        StateHandler();
    }

    // new stuff

    public void StateHandler(){
        if(walljumping){
            state = MovementState.walljumping;
        }else if(wallsliding){
            state = MovementState.wallsliding;
            speed = wallRunSpeed;
        }
        
    }
    private void MovePlayer(){

        // walk in direction that player is facing
        moveDirection = orientation.forward * upDown + orientation.right * leftRight;
        
         // on ground
        if(grounded) {
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        }
            

        // in air
        else if(!grounded) {
            rb.AddForce(moveDirection.normalized * airSpeed * 10f * airMultipler, ForceMode.Force);
        }
            
    }

    // limit movement bc player is flying all over the plce
    private void LimitSpeed(){
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit if movespeed goes over 
        // calc real max velocity + update 
        if(flatVelocity.magnitude > speed){
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);   
        }
    }
 
    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump(){
        jumpReady = true;
    }
}
