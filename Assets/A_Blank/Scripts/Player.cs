using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] Transform mesh;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] Joystick joystick;
    [SerializeField] [Range(0, 1)] float moveThreshold = 0.5f;

    [Header("Movment Modes")]
    [SerializeField] float moveSpeedSlow = 10;
    [SerializeField] Collider slowTrigger;
    [SerializeField] float moveSpeedNormal = 15;
    [SerializeField] Collider normalTrigger;
    [SerializeField] float moveSpeedFast = 20;
    [SerializeField] Collider fastTrigger;
    [SerializeField] TextMeshProUGUI currentSpeedText;


    private int movementMode;
    private float moveSpeed;


    private CharacterController cc;
    private float horizontalInput, verticalInput;
    private Vector3 yVel;

    private void Start() {
        cc = GetComponent<CharacterController>();
        
        
        movementMode = 0;
        moveSpeed = moveSpeedSlow;
        currentSpeedText.SetText("Slow");
        slowTrigger.gameObject.SetActive(true);
        normalTrigger.gameObject.SetActive(false);
        fastTrigger.gameObject.SetActive(false);


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

    public void ChangeMovement() {
        movementMode++;
        if(movementMode > 2)
            movementMode = 0;

        if(movementMode == 0) {
            moveSpeed = moveSpeedSlow;
            currentSpeedText.SetText("Slow");
            slowTrigger.gameObject.SetActive(true);
            normalTrigger.gameObject.SetActive(false);
            fastTrigger.gameObject.SetActive(false);
        } else if (movementMode == 1) {
            moveSpeed = moveSpeedNormal;
            currentSpeedText.SetText("Normal");
            normalTrigger.gameObject.SetActive(true);
            slowTrigger.gameObject.SetActive(false);
            fastTrigger.gameObject.SetActive(false);
        } else if (movementMode == 2) {
            moveSpeed = moveSpeedFast;
            currentSpeedText.SetText("Fast");
            fastTrigger.gameObject.SetActive(true);
            normalTrigger.gameObject.SetActive(false);
            slowTrigger.gameObject.SetActive(false);
        }
    }
}
