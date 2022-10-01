using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
  
    public float speed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCD;
    public float airMultipler;
    bool jumpReady;

    public Transform orientation;
    float leftRight;
    float upDown;

    [Header("onSurface")]

    public float playerHeight;
    public LayerMask Ground;
 
    bool grounded;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start(){
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpReady = true;
    }

    private void Update(){
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        

        PlayerInput();
        LimitSpeed();

        // ground drag
        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void PlayerInput(){
        leftRight = Input.GetAxisRaw("Horizontal");
        upDown = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && grounded && jumpReady){
            jumpReady = false;
            Jump();

            // jump over and over again whn pressing space
            Invoke(nameof(ResetJump), jumpCD);
        }
    }

    private void MovePlayer(){

        // walk in direction that player is facing
        moveDirection = orientation.forward * upDown + orientation.right * leftRight;
        
         // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultipler, ForceMode.Force);
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
