using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Investigate : AI_State
{
    private float distance;
    public override void InitilizeState(AI ai) {
        ai.agent.speed = ai.walkSpeed;
        ai.agent.SetDestination(ai.pointNoted);
        ai.myAnimator.SetBool("idle", false);
        ai.myAnimator.SetBool("walk", true);
    }

    public override void Exicute(AI ai) {
        distance = (ai.pointNoted - ai.transform.position).sqrMagnitude;
        if(distance < ai.arriveRadius * ai.arriveRadius)
            ai.SwitchState(connections[0]);
    }
}
