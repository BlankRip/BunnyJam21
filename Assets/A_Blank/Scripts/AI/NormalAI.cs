using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : AI
{
    Investigate investigateState;
    private void Start() {
        AiInitilize();

        Idle idleState = new Idle();
        Patrol patrolState = new Patrol();
        investigateState = new Investigate();

        idleState.connections.Add(patrolState);
        patrolState.connections.Add(idleState);
        investigateState.connections.Add(patrolState);

        SwitchState(idleState);
    }

    private void Update() {
        currentState.Exicute(this);
    }

    public override void RecievedPlayerPosition(Vector3 pos) {
        base.RecievedPlayerPosition(pos);
        SwitchState(investigateState);
    }
}
