using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : AI
{
    private void Start() {
        AiInitilize();
        
        Idle idleState = new Idle();
        Patrol patrolState = new Patrol();

        idleState.connections.Add(patrolState);
        patrolState.connections.Add(idleState);

        SwitchState(idleState);
    }

    private void Update() {
        currentState.Exicute(this);
    }
}
