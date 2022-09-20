using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
  
    public float speed;
    public float groundDrag;
    public Transform orientation;
    float leftRight;
    float upDown;

    [Header("onGround")]

    public float playerHeight;
    public LayerMask Ground;
 
    bool grounded;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start(){
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        
        PlayerInput();
        LimitSpeed();

        // ground drag
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void PlayerInput(){
        leftRight = Input.GetAxisRaw("Horizontal");
        upDown = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){

        // walk in direction that player is facing
        moveDirection = orientation.forward * upDown + orientation.right * leftRight;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
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
 
}
