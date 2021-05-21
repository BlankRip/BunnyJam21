using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform mesh;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] Joystick joystick;
    [SerializeField] [Range(0, 1)] float moveThreshold = 0.5f;
    [SerializeField] float watchCooldown = 10;

    [Header("Movment Modes")]
    [SerializeField] float moveSpeedSlow = 10;
    [SerializeField] Collider slowTrigger;
    [SerializeField] float moveSpeedNormal = 15;
    [SerializeField] Collider normalTrigger;
    [SerializeField] float moveSpeedFast = 20;
    [SerializeField] Collider fastTrigger;
    private float slowMax;
    private float normalMax;
    private float fastMax;


    private int movementMode;
    private float moveSpeed;
    private float moveClamp;
    private float cooldownRef;
    private bool watchUsed;


    [HideInInspector] public CharacterController cc;
    private float horizontalInput, verticalInput;
    private Vector3 yVel;
    private bool lockMovment;

    [HideInInspector] public Interactable interactable;
    [HideInInspector] public AI aiReadyToDie;

    private void Start() {
        cc = GetComponent<CharacterController>();
        UIManager.instance.InitilizeUI();
        
        slowMax = (((moveSpeedSlow/10)) * 0.2f);
        normalMax = (((moveSpeedNormal/10)) * 0.2f);
        fastMax = (((moveSpeedFast/10)) * 0.2f) + 0.1f;

        movementMode = 0;
        moveSpeed = moveSpeedSlow;
        moveClamp = slowMax;
        UIManager.instance.UpdateMoveMode("Slow");
        slowTrigger.gameObject.SetActive(true);
        normalTrigger.gameObject.SetActive(false);
        fastTrigger.gameObject.SetActive(false);
        

        yVel = Vector3.zero;
        if(gravity > 0) {
            gravity = gravity * -1;
        }
    }

    private void Update() {
        if(!GameManager.instance.paused && !lockMovment) {
            if(joystick.Horizontal > moveThreshold)
                horizontalInput = moveSpeed * GameManager.instance.delta;
            else if(joystick.Horizontal < -moveThreshold)
                horizontalInput = -moveSpeed * GameManager.instance.delta;
            else
                horizontalInput = 0;

            if(joystick.Vertical > moveThreshold)
                verticalInput = moveSpeed * GameManager.instance.delta;
            else if(joystick.Vertical < -moveThreshold)
                verticalInput = -moveSpeed * GameManager.instance.delta;
            else
                verticalInput = 0;
        } else if(horizontalInput != 0 || verticalInput != 0) {
            horizontalInput = verticalInput = 0;
        }

        if(watchUsed) {
            cooldownRef += GameManager.instance.delta;
            UIManager.instance.WatchRecovering(watchCooldown, cooldownRef);
            if(cooldownRef >= watchCooldown) {
                Time.timeScale = 1;
                cooldownRef = 0;
                //UIManager.instance.WatchReady();
                watchUsed = false;
            }
        }

        Movement();
    }

    private void Movement() {
        Vector3 move = (transform.forward * verticalInput + transform.right * horizontalInput);
        move.x = Mathf.Clamp(move.x, -moveClamp, moveClamp);
        move.z = Mathf.Clamp(move.z, -moveClamp, moveClamp);
        cc.Move(move);

        if(move != Vector3.zero)
            mesh.rotation = Quaternion.Slerp(mesh.rotation, Quaternion.LookRotation(move, Vector3.up), 
                GameManager.instance.delta * rotationSpeed);

        //^ Grav Sim
        if (cc.isGrounded && yVel.y < 0)
            yVel.y = -2;
        yVel.y += gravity * GameManager.instance.delta;
        cc.Move(yVel * GameManager.instance.delta);
    }
    public void LockMovement() {
        lockMovment = true;
    }
    public void UnlockMovement() {
        lockMovment = false;
    }

    public void Kill() {
        if(!lockMovment) {
            aiReadyToDie.Death();
        }
    }

    public void Interact() {
        interactable.OnInteraction();
    }

    public void UseWatch() {
        Time.timeScale = 0;
        UIManager.instance.WatchUsed();
        watchUsed = true;
    }

    public void ChangeMovement() {
        movementMode++;
        if(movementMode > 2)
            movementMode = 0;

        if(movementMode == 0) {
            moveSpeed = moveSpeedSlow;
            moveClamp = slowMax;
            UIManager.instance.UpdateMoveMode("Slow");
            if(!lockMovment) {
                slowTrigger.gameObject.SetActive(true);
                normalTrigger.gameObject.SetActive(false);
                fastTrigger.gameObject.SetActive(false);
            }
        } else if (movementMode == 1) {
            moveSpeed = moveSpeedNormal;
            moveClamp = normalMax;
            UIManager.instance.UpdateMoveMode("Normal");
            if(!lockMovment) {
                normalTrigger.gameObject.SetActive(true);
                slowTrigger.gameObject.SetActive(false);
                fastTrigger.gameObject.SetActive(false);
            }
        } else if (movementMode == 2) {
            moveSpeed = moveSpeedFast;
            moveClamp = fastMax;
            UIManager.instance.UpdateMoveMode("Fast");
            if(!lockMovment) {
                fastTrigger.gameObject.SetActive(true);
                normalTrigger.gameObject.SetActive(false);
                slowTrigger.gameObject.SetActive(false);
            }
        }
    }

    public void EnterHide() {
        mesh.gameObject.SetActive(false);
        lockMovment = true;
        fastTrigger.gameObject.SetActive(false);
        normalTrigger.gameObject.SetActive(false);
        slowTrigger.gameObject.SetActive(false);
    }
    public void ExitHide() {
        mesh.gameObject.SetActive(true);
        lockMovment = false;
        if(movementMode == 0)
            slowTrigger.gameObject.SetActive(true);
        else if (movementMode == 1)
            normalTrigger.gameObject.SetActive(true);
        else if (movementMode == 2)
            fastTrigger.gameObject.SetActive(true);
    }

    public void Zap() {
        //^Play Zap Effect
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(1);
        UIManager.instance.ShowEnd();
    }
}
