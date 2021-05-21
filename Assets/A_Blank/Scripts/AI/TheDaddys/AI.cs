using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    public Animator myAnimator;
    public float arriveRadius = 0.2f;
    public float idleTime = 3;
    public NavMeshAgent agent;
    public float walkSpeed = 3.5f;
    public float chaseSpeed = 5;
    public Transform[] waypoints;
    protected AI_State currentState;
    public bool loopWaypoints;

    public bool chasing;
    public Vector3 pointNoted;
    public Vector3 initialPos;
    public Quaternion initialRotation;

    protected void AiInitilize() {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        initialRotation = transform.rotation;
    }

    protected void StandardUpdates() {
        ViewCone();
    }

    public virtual void RecievedPlayerPosition(Vector3 pos) { 
        pointNoted.x = pos.x;
        pointNoted.y = transform.position.y;
        pointNoted.z = pos.z;
    }

    public void ViewCone() {
        if(!chasing) {

        }
    }

    public void Death() {

    }

    public void SwitchState(AI_State state) {
        if(currentState != null) 
            currentState.ExitState(this);
        currentState = state;
        currentState.InitilizeState(this);
    }
}
