using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : AI_State
{
    private float catchDistance = 1;
    private float distance;
    private float searchingFor;
    private Vector3 chasePos;
    private bool caughtPlayer;


    public override void InitilizeState(AI ai) {
        Debug.Log("Entered Chase");
        ai.inChaseState = true;
        chasePos = GameManager.instance.playerScript.transform.position;
        chasePos.y = ai.transform.position.y;
        ai.agent.speed = ai.chaseSpeed;
        ai.agent.SetDestination(chasePos);
        searchingFor = 0;
        caughtPlayer = false;
    }

    public override void Exicute(AI ai) {
        if(!caughtPlayer) {
            distance = (GameManager.instance.playerScript.transform.position - ai.transform.position).sqrMagnitude;
            if(distance <= catchDistance * catchDistance) {
                GameManager.instance.playerScript.LockMovement();
                GameManager.instance.playerScript.Zap();
                caughtPlayer = true;
                return;
            }

            chasePos = GameManager.instance.playerScript.transform.position;
            chasePos.y = ai.transform.position.y;
            ai.agent.SetDestination(chasePos);

            if(!ai.chasing) {
                if(!GameManager.instance.paused)
                    searchingFor += GameManager.instance.delta;
                if(searchingFor >= ai.searchTime) {
                    searchingFor = 0;
                    ai.SwitchState(connections[0]);
                }
            } else if(searchingFor != 0)
                searchingFor = 0;
        }
    }

    public override void ExitState(AI ai) {
        ai.inChaseState = false;
        //ai.chasing = false;
    }
}