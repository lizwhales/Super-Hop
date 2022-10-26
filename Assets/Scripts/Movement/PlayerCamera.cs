using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
  
    public float xSense;
    public float ySense;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSense;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySense;

        // to handle the rotations in unity 
        yRotation += mouseX;
        xRotation -= mouseY;

        // clamp to restrict looking up and down more than 90 deg
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


    }

}

