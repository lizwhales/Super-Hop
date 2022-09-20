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
 
}
