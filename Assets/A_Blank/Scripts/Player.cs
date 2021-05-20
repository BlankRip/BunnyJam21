using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform mesh;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] Joystick joystick;
    [SerializeField] [Range(0, 1)] float moveThreshold = 0.5f;

    private CharacterController cc;
    private float horizontalInput, verticalInput;
    private Vector3 yVel;

    private void Start() {
        cc = GetComponent<CharacterController>();
        yVel = Vector3.zero;
        if(gravity > 0) {
            gravity = gravity * -1;
        }
    }

    private void Update() {
        if(joystick.Horizontal > moveThreshold)
            horizontalInput = moveSpeed * Time.deltaTime;
        else if(joystick.Horizontal < -moveThreshold)
            horizontalInput = -moveSpeed * Time.deltaTime;
        else
            horizontalInput = 0;

        if(joystick.Vertical > moveThreshold)
            verticalInput = moveSpeed * Time.deltaTime;
        else if(joystick.Vertical < -moveThreshold)
            verticalInput = -moveSpeed * Time.deltaTime;
        else
            verticalInput = 0;

        Movement();
    }

    private void Movement() {
        Vector3 move = (transform.forward * verticalInput + transform.right * horizontalInput);
        cc.Move(move);

        if(move != Vector3.zero)
            mesh.rotation = Quaternion.Slerp(mesh.rotation, Quaternion.LookRotation(move, Vector3.up), Time.deltaTime * rotationSpeed);

        //^ Grav Sim
        if (cc.isGrounded && yVel.y < 0)
            yVel.y = -2;
        yVel.y += gravity * Time.deltaTime;
        cc.Move(yVel * Time.deltaTime);
    }
}
