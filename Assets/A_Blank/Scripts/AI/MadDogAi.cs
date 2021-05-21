using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDogAi : AI
{
    Investigate investigateState;

    private void Start() {
        AiInitilize();

        Idle idleState = new Idle();
        Patrol patrolState = new Patrol();
        investigateState = new Investigate();
        chaseState = new Chase();

        idleState.connections.Add(patrolState);
        patrolState.connections.Add(idleState);
        investigateState.connections.Add(patrolState);
        chaseState.connections.Add(patrolState);

        currentState = idleState;
        currentState.InitilizeState(this);
    }

    private void Update() {
        if(!dead) {
            StandardUpdates();
            currentState.Exicute(this);
        }
    }

    public override void RecievedPlayerPosition(Vector3 pos, bool isPlayer) {
        if(!inChaseState) {
            if(isPlayer) {
                SwitchState(chaseState);
            } else {
                base.RecievedPlayerPosition(pos, isPlayer);
                SwitchState(investigateState);
            }
        }
    }
}
