using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    public Animator myAnimator;
    public NavMeshAgent agent;
    public Transform[] waypoints;
    protected AI_State currentState;
    public bool chasing;
    public bool loop;

    protected void AiInitilize() {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void StandardUpdates() {
        ViewCone();
    }

    public virtual void RecievedPlayerPosition(Vector3 pos) { }

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
